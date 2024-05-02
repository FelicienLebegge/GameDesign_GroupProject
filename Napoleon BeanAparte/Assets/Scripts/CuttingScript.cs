using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    [SerializeField] private KitchenStates _kitchenStateScripts;
    private bool isCutting = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }
}
