using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using static KitchenStates;
using Unity.VisualScripting;

public class Bean : MonoBehaviour
{
    [SerializeField] private KitchenStates.CookingStation _cookingStation;
    
    [SerializeField] 
    private float _moveSpeed = 3f;
    [SerializeField]
    private Vector3 _beanPosition = new(0,1.6f, -4.5f);
     
    private Vector4 _offset = new(2,4,6,8);
    private bool _isBeanMoving;

    private bool _hasBeenAddedToList = false;
    public enum BeanTypes //assigned to each bean in the inspector
    {
        Pea,
        Navy,
        Fava,
        Anasazi,
        French
    }

    public BeanTypes BeanType;

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
                    break;
            case KitchenStates.CookingStation.Cutting:
                break;
            case KitchenStates.CookingStation.Cooking:
                break;
        }
    }

    
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Dirt")) //fixes a bug where the list kept growing once a bean is moved out
        {
            _isBeanMoving = true;
        }
    }

    public void MoveBean()
    {
        if (transform.position != _beanPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _beanPosition, Time.deltaTime * _moveSpeed);
        }
        else
        {
            // Bean has reached the target position
            _isBeanMoving = false;
            AddBean();
        }
    }

    private void AddBean() //prevents a bug where the beans kept on adding to the list each frame
    {
        if (!_hasBeenAddedToList)
        {
            KitchenStates.BeansList.Add(this.gameObject);
            _hasBeenAddedToList = true;
        }
    }
}

    
