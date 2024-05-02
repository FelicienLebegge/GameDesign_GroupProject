using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using static KitchenStates;
using Unity.VisualScripting;

public class Bean : MonoBehaviour
{
    KitchenStates _kitchenStates;

    [SerializeField] private KitchenStates.CookingStation _cookingStation;
    
    [SerializeField] 
    private float _moveSpeed = 3f;
    [SerializeField]
    private Vector3 _beanPosition = new(0,1.6f, -4.5f);
     
    private Vector4 _offset = new(2,4,6,8);
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
        switch (_cookingStation)
        {
            case KitchenStates.CookingStation.Washing:
                if(_isBeanMoving)
                {
                    MoveBean();
                };
                if(_isSelectable)
                {
                    AddBean();
                }
                    break;
            case KitchenStates.CookingStation.Cutting:

                break;
            case KitchenStates.CookingStation.Cooking:
                break;
        }
    }

    private void AddBean()
    {
        _kitchenStates.ActiveBeansList.Add(this);
    }

    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Dirt"))
        {
            _isBeanMoving = true;
            
        }
    }

    private void MoveBean()
    {
        switch (BeanType)
        {
            case BeanTypes.Pea:
                transform.position = Vector3.Lerp(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Navy:
                transform.position = Vector3.Lerp(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Fava:
                transform.position = Vector3.Lerp(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.Anasazi:
                transform.position = Vector3.Lerp(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
                break;
            case BeanTypes.French:
                transform.position = Vector3.Lerp(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
                break;

                
        }
        _isSelectable = true;
        //random unit circle
        //clipping niet erg als types dezelfde zijn

    }
}


