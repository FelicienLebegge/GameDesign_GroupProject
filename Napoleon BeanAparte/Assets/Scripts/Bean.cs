using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using static KitchenStates;
using Unity.VisualScripting;

public class Bean : MonoBehaviour
{
    [SerializeField] private KitchenStates.CookingStation _kitchenStates;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _beanPosition;
    [SerializeField] private Transform _beanlocation;
    [SerializeField] private Vector4 _offset = new(2,4,6,8);
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
    private bool _isSelectable = false;


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
     
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.CompareTag("Dirt"))
        {
            MoveBean();
            _isSelectable = true;
        }
    }

    private void MoveBean()
    {
        switch (BeanType)
        {
            case BeanTypes.Pea:
                transform.position = Vector3.Lerp(transform.position, new(0,0,0), Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Navy:
                transform.position = Vector3.Lerp(transform.position, _beanPosition.position, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Fava:
                transform.position = Vector3.Lerp(transform.position, _beanPosition.position, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Anasazi:
                transform.position = Vector3.Lerp(transform.position, _beanPosition.position, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.French:
                transform.position = Vector3.Lerp(transform.position, _beanPosition.position, Time.deltaTime * _moveSpeed);
                break;
        }
        //random unit circle
        //clipping niet erg als types dezelfde zijn

    }
}


