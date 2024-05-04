using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPan : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _originalPosition;

    private bool _isDragging;
    private bool _isSnapping;

    private float _distancefromCamera = 0.1f;

    [SerializeField] private Transform _snapPosition;
    [SerializeField] private float _snapThreshold = 1f;
    [SerializeField] private float _snapSpeed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (KitchenStates.IsCuttingDone) //only when the cutting is done may the pan move, otherwise people can cheat the washing system
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject) //check if pan is under mouse click
                    {
                        _isDragging = true;
                        _isSnapping = false;
                    }
                }
            }

            if (_isDragging)
            {
                Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.y = transform.position.y; //keep the pan at the same height

                transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * _snapSpeed);

                UpdateBeanRigidBodies();
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                SnapToCookingPlate();
            }

            if (_isSnapping)
            {
                transform.position = Vector3.Lerp(transform.position, _snapPosition.position, Time.deltaTime * _snapSpeed);
            }
        }
    }

    private void SnapToCookingPlate()
    {
        float distanceToSnapPoint = Vector3.Distance(transform.position, _snapPosition.position);
        if (distanceToSnapPoint <= _snapThreshold)
        {
            _isSnapping = true;
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void UpdateBeanRigidBodies()
    {
        foreach (GameObject bean in KitchenStates.BeansList)
        {
            Rigidbody rigidbody = bean.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.isKinematic = true;
            }
        }
    }

    void ReturnToOriginalPosition()
    {
        transform.position = _originalPosition;
    }
}
