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
    private float _speed = 0.5f;

    private void Update()
    {
        Debug.Log(KitchenStates.BeansList.Count);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartTreadmill();
        }
    }

    IEnumerator TreadMillCoroutine()
    {
        foreach (GameObject bean in KitchenStates.BeansList)
        {
            bean.transform.position = _start.position;
        }

            foreach (GameObject bean in KitchenStates.BeansList)
        {
            float t = 0f;

            Vector3 startPosition = bean.transform.position;

            while (t < 1f)
            {
                t += Time.deltaTime * _speed;

                t = Mathf.Clamp01(t);

                bean.transform.position = Vector3.Lerp(startPosition, _target.position, t);

                yield return null;
            }

            // Once the interpolation is complete, set the bean's position to the target position
            bean.transform.position = _target.position;
        }
    }

    public void StartTreadmill()
    {
        StartCoroutine(TreadMillCoroutine());
    }
}
