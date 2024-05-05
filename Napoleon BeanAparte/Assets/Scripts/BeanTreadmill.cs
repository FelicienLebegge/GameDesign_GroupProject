using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeanTreadmill : MonoBehaviour
{

    [SerializeField]
    private Transform _start;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _secondTarget; //the position above the pan

    [SerializeField]
    private float _speed = 0.5f;

    [SerializeField]
    private float _randomOffsetRange = 0.1f; // range around second target so the beans fall more naturaly

    private bool _hasTreadmillStarted = false;

    private void Update()
    {
        Debug.Log(KitchenStates.BeansList.Count);

        if (KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting && KitchenStates.AreBeansWashed && !_hasTreadmillStarted) //when the beans are washed and cutting is set to active start the treadmill
        {
            StartTreadmill();
            _hasTreadmillStarted = true;
        }
    }

    IEnumerator StartBeans()
    {
        float maxDelay = 1f; 
        float minDelay = 0.5f; 
        float delayMultiplier = 0.5f; 

        foreach (GameObject bean in KitchenStates.BeansList)
        {
            float delay = maxDelay / (KitchenStates.BeansList.Count * delayMultiplier); //make the delay dependent on the amount of beans --> The more beans the less delay
            delay = Mathf.Clamp(delay, minDelay, maxDelay);

            StartCoroutine(TreadMillCoroutine(bean, delay));
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator TreadMillCoroutine(GameObject bean, float delay)
    {
        bean.transform.position = _start.position;

        yield return new WaitForSeconds(delay);

        Vector3 startPosition = bean.transform.position;
        Vector3 firstTargetPosition = _target.position;


        yield return MoveBean(bean, startPosition, firstTargetPosition); //move to first target, with delay

        Vector3 secondTargetPosition = _secondTarget.position + Random.insideUnitSphere * _randomOffsetRange;
        bean.transform.position = secondTargetPosition; //teleport the bean to the pan

        Rigidbody rigidbody = bean.GetComponent<Rigidbody>(); //set the rigidbody active
        rigidbody.isKinematic = false;

        Collider beanCollider = bean.GetComponent<Collider>(); //make sure the collider is not on trigger anymore to allow collisions
        beanCollider.isTrigger = false;

        KitchenStates.IsCuttingDone = true;

    }

    IEnumerator MoveBean(GameObject bean, Vector3 startPosition, Vector3 targetPosition)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * _speed;

            t = Mathf.Clamp01(t);

            bean.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // Once the interpolation is complete, set the bean's position to the target position
        bean.transform.position = targetPosition;
    } //Move beans from a to b

    public void StartTreadmill()
    {
        StartCoroutine(StartBeans());
    }
}
