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
    private bool IsNotAccesible;

    [SerializeField] private KitchenStates _kitchenStates;

    private Vector3 _targetPosition;

    [SerializeField]
    private int _index;

    private DrawCut _drawCut;
    private MouseClickCut _mouseClickCut;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = _cams[0].transform.position;
        _drawCut = gameObject.GetComponent<DrawCut>();
        _mouseClickCut = gameObject.GetComponent<MouseClickCut>();

        _buttons[0].GetComponent<Image>().color = Color.green;
        _buttons[1].GetComponent<Image>().color = Color.grey;
        _buttons[2].GetComponent<Image>().color = Color.grey;

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
        AudioManager.instance.Play("Woosh");
        _isLerping = true;
        if (i == 0)
        {
            if (!IsNotAccesible)
            {
                _index = i;
            }
        }
        else _index = i;
        _elapsedTime = 0;

        //update kitchen state according to active station
        switch (_index)
        {
            case 0:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Washing);
                _drawCut.enabled = false;
                _buttons[0].GetComponent<Image>().color = Color.green;
                _buttons[1].GetComponent<Image>().color = Color.grey;
                _buttons[2].GetComponent<Image>().color = Color.grey;
                //_mouseClickCut.enabled = false;
                break;
            case 1:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cutting);
                _drawCut.enabled = true;
                _buttons[1].GetComponent<Image>().color = Color.green;
                _buttons[0].GetComponent<Image>().color = Color.grey;
                _buttons[2].GetComponent<Image>().color = Color.grey;
                //_mouseClickCut.enabled = true;
                break;
            case 2:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cooking);
                _drawCut.enabled = false;
                _buttons[2].GetComponent<Image>().color = Color.green;
                _buttons[0].GetComponent<Image>().color = Color.grey;
                _buttons[1].GetComponent<Image>().color = Color.grey;
                //_mouseClickCut.enabled = false;
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

    public void StartCameraLerp(int index)
    {
        if (index < 0 || index >= _cams.Length)
        {
            Debug.LogWarning("Invalid camera index.");
            return;
        }

        _targetPosition = _cams[index].transform.position;
        _isLerping = true;
        _elapsedTime = 0f;

        _index = index;


        switch (index)
        {
            case 0:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Washing);
                _drawCut.enabled = false;
                _buttons[0].GetComponent<Image>().color = Color.green;
                _buttons[1].GetComponent<Image>().color = Color.grey;
                _buttons[2].GetComponent<Image>().color = Color.grey;
                //_mouseClickCut.enabled = false;
                break;
            case 1:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cutting);
                _drawCut.enabled = true;
                //_mouseClickCut.enabled = true;
                break;
            case 2:
                _kitchenStates.SetKitchenState(KitchenStates.CookingStation.Cooking);
                _drawCut.enabled = false;
                //_mouseClickCut.enabled = false;
                break;
        }
    }
    public void AccessabilityChange(bool acces)
    {
        IsNotAccesible = acces;
    }
}
