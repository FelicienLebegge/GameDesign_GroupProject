using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CuttingScript : MonoBehaviour
{
    [SerializeField] private KitchenStates _kitchenStateScripts;

    private bool isCutting = false;

    [SerializeField]
    private Transform _kitchenKnife;

    [SerializeField]
    private float _knifeDownSpeed = 5f;

    [SerializeField]
    private float _knifeupSpeed = 2f;

    [SerializeField]
    private int partialMissPoints = 15;

    [SerializeField]
    private int completeMissPoints = 2;

    // Start is called before the first frame update
    void Awake()
    {
         _kitchenKnife = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_kitchenStateScripts.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            if(Input.GetMouseButtonDown(0) & !isCutting)
            {
                KnifeDown();
                isCutting = true;
                Debug.Log("Click detected");
            }
        }
    }

    private void KnifeDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _knifeDownSpeed);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Bean"))
            {
                GetPoints();
                KnifeUp();
            }
            if (hit.collider.CompareTag("table"))
            {
                KnifeUp();
            }
        }
    }

    private void KnifeUp()
    {
        transform.position = Vector3.Lerp(transform.position, _kitchenKnife.position, Time.deltaTime * _knifeupSpeed);
    }

    private void GetPoints()
        {
        throw new NotImplementedException();
        }

    public class CutMechanic : MonoBehaviour
    {
        
        bool IsAccuratelyCut(GameObject target)
        {
            // Check if the target is destroyed
            // You may want to replace this with more sophisticated logic based on your game's requirements
            return target == null;
        }

        // Method to check if the target is partially missed
        bool IsPartiallyMissed(GameObject target)
        {
            // Check if the target still exists
            // You may want to replace this with more sophisticated logic based on your game's requirements
            return target != null;
        }

        // Method to increase player points
        void IncreasePoints(int points)
        {
            // Implement your points management logic here
            // For demonstration, let's just print the points to the console
            Debug.Log("Points Increased: " + points);
        }

        
    }
}
