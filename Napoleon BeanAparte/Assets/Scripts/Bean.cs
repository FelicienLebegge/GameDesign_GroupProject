using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using static KitchenStates;

public class Bean : MonoBehaviour
{
    [SerializeField] private KitchenStates.CookingStation _kitchenStates;
    public enum BeanTypes //assigned to each bean in the inspector
    {
        Pea,
        Navy,
        Fava,
        Anasazi,
        French
    }

    public BeanTypes BeanType;
    private Collider _bean;
    public int BeanValue;

    void Start()
    {
        Collider _bean = GetComponent<Collider>();
    }

    private void Update()
    {
        switch (_kitchenStates)
        {
            case KitchenStates.CookingStation.Washing:

                break;  
            case KitchenStates.CookingStation.Cutting:
                break;
            case KitchenStates.CookingStation.Cooking:
                break;
        }
    }

}

