using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CuttingScript : MonoBehaviour
{
    [SerializeField] private KitchenStates _kitchenStateScripts;

    private bool _isCutting = false;

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
   
    private Camera _mainCamera;
    private Vector3 _originalPosition;

    // Start is called before the first frame update
    void Awake()
    {
        _kitchenKnife = GetComponent<Transform>();
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_kitchenStateScripts.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            HandleInputs();
            
            if (_isCutting)
            {
                KnifeDown();
            }
            else                                  //allows for smooth movement to _originalPosition
            {
                _isCutting = false;
                //ReturnToOriginalPosition();
            }
        }
    }
    private void HandleInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("cut detected");
                    _isCutting = true;
                    
                }
            }
        }

       
    }
    private void KnifeDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _knifeDownSpeed);

        RaycastHit ahit;
        if (Physics.Raycast(transform.position, Vector3.down, out ahit, Mathf.Infinity))
        {
            if (ahit.collider.CompareTag("Bean"))
            {
                GetPoints();
                KnifeUp();
                _isCutting = false;
            }
            if (ahit.collider.CompareTag("Table"))
            {
                KnifeUp();
                _isCutting = false;
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

    private void ReturnToOriginalPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _originalPosition, Time.deltaTime * _knifeupSpeed);
    }
}

