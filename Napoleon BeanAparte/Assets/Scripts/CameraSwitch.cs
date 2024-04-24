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

    private Vector3 _targetPosition;

    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = _cams[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isLerping = true;
            _index++;

            if (_index >= _cams.Length)
            {
                _index = 0;
            }

            _elapsedTime = 0;
        }

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

    private void UpdateCamera(float completionPercentage)
    {
        _targetPosition = _cams[_index].transform.position;

        Vector3 currentPosition = Camera.main.transform.position;
        Quaternion currentRotation = Camera.main.transform.rotation;
        float currentFOV = Camera.main.fieldOfView;

        Camera.main.transform.position = Vector3.Lerp(currentPosition, _targetPosition, completionPercentage);
    }
}
