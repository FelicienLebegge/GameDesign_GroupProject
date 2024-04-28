using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using static KitchenStates;
using Unity.VisualScripting;

public class Bean : MonoBehaviour
{
    [SerializeField] private KitchenStates.CookingStation _kitchenStates;
   
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _beanPosition;
    [SerializeField] private Vector4 _offset = new(2,4,6,8);
    private bool _isBeanMoving;
    public enum BeanTypes //assigned to each bean in the inspector
    {
        Pea,
        Navy,
        Fava,
        Anasazi,
        French
    }

    public BeanTypes BeanType;

   
    private bool _isSelectable = false;


    public int BeanValue;


    
    void Start()
    {
        
    }

    private void Update()
    {
        switch (_kitchenStates)
        {
            case KitchenStates.CookingStation.Washing:
                if(_isBeanMoving)
                {
                    MoveBean();
                };
                    break;
            case KitchenStates.CookingStation.Cutting:

                break;
            case KitchenStates.CookingStation.Cooking:
                break;
        }
    }
     
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Dirt"))
        {
            _isBeanMoving = true;
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


