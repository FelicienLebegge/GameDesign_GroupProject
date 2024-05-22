using TMPro;
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

    [SerializeField]
    private Transform _loc;

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
            _isCutting = false;
        }
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.CompareTag("Table"))
        {
            _isCutting = false;
        }
        
    }
}

