using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private Camera[] _cams;

    [SerializeField]
    private Button[] _buttons;

    [SerializeField]
    private float _lerpDuration = 5.0f;

    private float _elapsedTime;
    private bool _isLerping;

    [SerializeField] private KitchenStates _kitchenStates;

    private Vector3 _targetPosition;    

    [SerializeField]
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = _cams[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _buttons[0].onClick.AddListener(() => OnButtonClick(0));
        _buttons[1].onClick.AddListener(() => OnButtonClick(1));
        _buttons[2].onClick.AddListener(() => OnButtonClick(2));

        if (_isLerping)
        {
            _elapsedTime += Time.deltaTime;
            float completionPercentage = _elapsedTime / _lerpDuration; //get value between 0 and 1

            UpdateCamera(completionPercentage);

            if (completionPercentage >= 1.0f) //is lerp complete?
            {
                _isLerping = false;
                _elapsedTime = 0f;
            }
        }
    }

    void OnButtonClick(int i)
    {
        
        _isLerping = true;
        _index = i;
        _elapsedTime = 0;

        //update kitchen state according to active station
        switch (_index)
        {
            case 0:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Washing);
                break;
            case 1:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cutting);
                break;
            case 2:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cooking);
                break;
        }
    }

    private void UpdateCamera(float completionPercentage)
    {
        _targetPosition = _cams[_index].transform.position;

        Vector3 currentPosition = Camera.main.transform.position;
        Quaternion currentRotation = Camera.main.transform.rotation;
        float currentFOV = Camera.main.fieldOfView;

        Camera.main.transform.position = Vector3.Lerp(currentPosition, _targetPosition, completionPercentage);
    }
}
