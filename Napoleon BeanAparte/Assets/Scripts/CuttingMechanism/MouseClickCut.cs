using TMPro;
using UnityEngine;

public enum Angle
{
    Up,
    Forward,
    Right
}
public class MouseClickCut : MonoBehaviour
{

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _miss;

    [SerializeField] private int _points = 18;

    [Header("Timers")]
    private float _timer = 0f;
    private float _missTimer = 0f;
    private bool _isMissTimerActive;
    private bool _isTimerActive;

    public Angle angle;

    void Update()
    {
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
        if (_isTimerActive)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _text.enabled = false;
                _timer = 0;
                _isTimerActive = false;
            }
        }
        RaycastHit hit;
        if (KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out hit, 10, 1 << 2) && hit.transform.tag == "Bean")
                {
                    Debug.Log("Object can be cut");

                    GameObject victim = hit.collider.gameObject;
                    if (victim.tag == "Bean")
                    {
                        if (victim.name == "French bean")
                        {
                            GetPoints();
                            Debug.Log("Double points GOLDEN BEAT");
                        }
                        AudioManager.instance.Play("BeanCut");
                        _timer = 0f;
                        GetPoints();
                        _text.enabled = true;
                        _isTimerActive = true;

                        if (_timer >= 0.5f)
                        {
                            _text.enabled = false;
                            _timer = 0;
                        }
                        if (angle == Angle.Up)
                        {
                            Cutter.Cut(victim, hit.point, Vector3.up);

                        }
                        else if (angle == Angle.Forward)
                        {
                            Cutter.Cut(victim, hit.point, Vector3.forward);

                        }
                        else if (angle == Angle.Right)
                        {
                            Cutter.Cut(victim, hit.point, Vector3.right);

                        }
                    }
                }
                else
                {
                    AudioManager.instance.Play("CutMiss");
                    _missTimer = 0f;
                    _miss.enabled = true;
                    _isMissTimerActive = true;

                    if (_missTimer >= 0.5f)
                    {
                        _miss.enabled = false;
                        _missTimer = 0;
                    }
                }
            }
        }
    }
    private void GetPoints()
    {
        KitchenStates.Score += _points;
        Debug.Log("Added 20 points");
    }
}
