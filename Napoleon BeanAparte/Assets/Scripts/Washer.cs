using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Washer : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed = 10f;

    [SerializeField]
    private float _distanceFromCamera = 2f;

    private GameObject _dirt; //will get assigned in ExecuteWashing() method

    private Vector3 _originalPosition;

    private bool _isDragging;
    private bool _isWashingDirt;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        if (!_isDragging && _isWashingDirt) // If not dragging and washing dirt, check if we are still hovering over dirt
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit) || !hit.collider.CompareTag("Dirt"))
            {
                _isWashingDirt = false; // If not hovering over dirt, stop washing
            }
        }

        if (_isDragging &&_isWashingDirt)
        {
            WashDirt();
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
                if (hit.collider.gameObject == gameObject) //check if washer is under mouse click
                {
                    _isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            ExecuteWashing();
        }
        else                                  //allows for smooth movement to _originalPosition
        {
            _isWashingDirt = false;
            ReturnToOriginalPosition();
        }
    }

    private void ExecuteWashing()
    {
        //make washer follow the mouse
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera)); //.y because topdown
        transform.position = mousePosition;

        // Define layer mask for dirt
        int dirtLayerMask = 1 << LayerMask.NameToLayer("Dirt"); 
            //https://docs.unity3d.com/ScriptReference/LayerMask.NameToLayer.html
            //https://learn.microsoft.com/en-us/cpp/cpp/left-shift-and-right-shift-operators-input-and-output?view=msvc-170


        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); //check if we are above dirt
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, dirtLayerMask))
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Dirt"))
            {
                _dirt = other; //make sure to assign _dirt
                _isWashingDirt = true;
            }
            else
            {
                _isWashingDirt = false; // If not above dirt --> stop washing
            }
        }
        else
        {
            _isWashingDirt = false; // If not hitting anything --> stop washing
        }
    }

    private void WashDirt()
    {
        Vector3 shrunkenScale = _dirt.transform.localScale * 0.9999f; //shrink by very small amount
        _dirt.transform.localScale = shrunkenScale;
    }

    private void ReturnToOriginalPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _originalPosition, Time.deltaTime * _moveSpeed);
    }
}
