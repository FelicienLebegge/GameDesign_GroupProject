using System.Threading;
using TMPro;
using TreeEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    [SerializeField] private KitchenStates _kitchenStateScripts;

    private bool _isCutting = false;
    private bool _canCut = true;


    [SerializeField]
    private Transform _kitchenKnife;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _miss;

    [Header("KnifeSpeed")]
    [SerializeField]
    private float _knifeDownSpeed = 20f;

    [SerializeField]
    private float _knifeUpSpeed = 10f;

    [SerializeField]
    private int _points = 18;

    //[SerializeField]
    //private int completeMissPoints = 2;
   
    private Camera _mainCamera;
    private Vector3 _originalPosition;
    private Rigidbody _rb;

    [SerializeField]
    private Transform _loc;

    [Header("Timers")]
    private float _timer = 0f;
    private float _missTimer = 0f;
    private bool _isMissTimerActive;
    private bool _isTimerActive;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _kitchenKnife = GetComponent<Transform>();
        _mainCamera = Camera.main;
        _originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            HandleInputs();

            if (_isCutting && _canCut)
            {
                transform.position = Vector3.Lerp(transform.position, _loc.transform.position, Time.deltaTime * _knifeDownSpeed);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _originalPosition, Time.deltaTime * _knifeUpSpeed);

                if (transform.position == _originalPosition)
                {
                    _canCut = true;
                }
                else
                {
                    _canCut = false;
                }
            }
            



            if (_isMissTimerActive)
            {
                _missTimer += Time.deltaTime;

                if (_missTimer >= 0.2f)
                {
                    _miss.enabled = false;
                    _missTimer = 0;
                    _isMissTimerActive = false;
                }
            }
            if(_isTimerActive)
            {
                _timer += Time.deltaTime;
                if (_timer >= 0.2f)
                {
                    _text.enabled = false;
                    _timer = 0;
                    _isTimerActive = false;
                }
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
    
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("Bean"))
        {
            AudioManager.instance.Play("BeanCut");
            _timer = 0f;
            GetPoints();
            _isCutting = false;
            _text.enabled = true;
            _isTimerActive = true;

            if (_timer >= 0.5f) 
            {
                _text.enabled = false;
                _timer = 0;
                
            }
        }
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("Table"))
        {
            AudioManager.instance.Play("CutMiss");
            _missTimer = 0f;
            _isCutting = false;
            _miss.enabled = true;
            _isMissTimerActive = true;
            

            if (_missTimer >= 0.5f)    
            {
                _miss.enabled = false;
                _missTimer = 0;
            }
            
        }
    }

        

    private void GetPoints()
    {
        KitchenStates.Score += _points;
        Debug.Log("Added 20 points");
    }
}

