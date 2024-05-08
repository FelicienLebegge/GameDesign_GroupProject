using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingPan : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _originalPosition;

    private bool _isDragging;
    private bool _isSnapping;

    private Transform _targetSnappingPosition;
    private Transform[] _possibleSnapTargets;

    [SerializeField] private Transform _cookingSnapPosition01;
    [SerializeField] private Transform _cookingSnapPosition02;
    [SerializeField] private Transform _cookingSnapPosition03;
    [SerializeField] private float _snapThreshold = 0.5f;
    [SerializeField] private float _snapSpeed = 5f;

    private float _cookingTime = 0f;
    [SerializeField] private float _rawThreshold = 10f; // Time in seconds for raw stage
    [SerializeField] private float _cookedThreshold = 20f; // Time in seconds for cooked stage, 5 seconds to get it off the stove
    [SerializeField] private float _burnedThreshold = 25f; // Time in seconds for burned stage

    [SerializeField] private float _colorChangeSpeed = 3f;

    [SerializeField] private Material _rawMaterial;
    [SerializeField] private Material _cookedMaterial;
    [SerializeField] private Material _burnedMaterial;

    [SerializeField] private Material _defaultMaterial;

    [SerializeField] private Transform _collectorStart;
    [SerializeField] private Transform _collectorEnd;

    private float _collectDuration = 0.5f;
    private float _collectStartTime;

    [SerializeField] private Transform _trash;


    private Renderer _renderer;

    private bool _isCooking;
    private bool _isCollecting;
    private bool _isTrashing;
    private int _cookingpoints;

    // Start is called before the first frame update
    void Awake()
    {
        Transform pan = transform.Cast<Transform>().FirstOrDefault(child => child.CompareTag("Pan")); //get a child with pan tag

        if (pan != null)
        {
            _renderer = pan.GetComponent<Renderer>();
        }

        _possibleSnapTargets = new Transform[] { _cookingSnapPosition01, _cookingSnapPosition02, _cookingSnapPosition03, _collectorStart, _trash }; //fill array with all possible snappingtargets

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

                UpdateBeanRigidBodies(); //turn off the rigidbodies to prevent beans spilling over
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                SnapToCookingSpot();

            }

            if (_isSnapping)
            {
                transform.position = Vector3.Lerp(transform.position, _targetSnappingPosition.position, Time.deltaTime * _snapSpeed);
            }

            if(_isCooking)
            {
                CheckCookingStage();
            }

            if(_isCollecting)
            {
                float collectProgress = (Time.time - _collectStartTime) / _collectDuration;

                transform.position = Vector3.Lerp(transform.position, _collectorEnd.position, Time.deltaTime * _snapSpeed);

                if(collectProgress > _collectDuration) //give some time to let the lerp play out
                
                ResetPan();

                KitchenStates.Score += _cookingpoints;
                KitchenStates.IsOrderCompleted = true;
            }

            if (_isTrashing)
            {
                ResetPan();
            }   
        }
    }

    

    private void SnapToCookingSpot()
    {
        bool snapped = false;

        foreach (Transform snappingPoint in _possibleSnapTargets)
        {
            Debug.Log(snappingPoint.name);
            float distanceToSnapPoint = Vector3.Distance(transform.position, snappingPoint.position);

            if (distanceToSnapPoint <= _snapThreshold)
            {
                // If close to any snapping point, snap to that point
                _isSnapping = true;
                _targetSnappingPosition = snappingPoint;
                snapped = true;

                if (_targetSnappingPosition == _cookingSnapPosition01 ||
                    _targetSnappingPosition == _cookingSnapPosition02 ||
                    _targetSnappingPosition == _cookingSnapPosition03)
                {
                    _isCooking = true;
                } 
                else if (_targetSnappingPosition == _collectorStart)
                {
                    _isCollecting = true;
                    _collectStartTime = Time.time;
                }

                else if(_targetSnappingPosition == _trash)
                {
                    _isTrashing = true;
                }

                break; //get out of here once a snapping point is found
            }
        }

        if (!snapped)
        {
            _isCooking = false;
            _isCollecting = false;
            _isTrashing = false;

            ReturnToOriginalPosition();
        }
    }

    private void UpdateBeanRigidBodies()
    {
        foreach (Bean bean in KitchenStates.BeansList)
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

    private void CheckCookingStage()
    {

        _cookingTime += Time.deltaTime;

        if (_cookingTime >= _burnedThreshold)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _burnedMaterial.color, Time.deltaTime * _colorChangeSpeed);
            Debug.Log("The dish is burned!");
            _cookingpoints = 5;

        }
        else if (_cookingTime >= _cookedThreshold)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _cookedMaterial.color, Time.deltaTime * _colorChangeSpeed);
            Debug.Log("The dish is perfectly cooked!");
            _cookingpoints = 25;
        }
        else if (_cookingTime >= _rawThreshold)
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _rawMaterial.color, Time.deltaTime * _colorChangeSpeed);
            Debug.Log("The dish is still raw!");
            _cookingpoints = 10;
        }
    }

    private void ResetPan()
    {
        _cookingTime = 0;
        _renderer.material.color = _defaultMaterial.color; //go back to default material

        foreach(Bean bean in KitchenStates.BeansList)
        {
            Destroy(bean.gameObject);
        }

        KitchenStates.BeansList.Clear();
        ReturnToOriginalPosition();
    }
}
