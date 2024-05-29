using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static TMP_InputField inputfield;
    public static Button _OK;
    [SerializeField] private TextMeshProUGUI _playername;
    [SerializeField] private TextMeshProUGUI _placeholderInside;
    [SerializeField] private TextMeshProUGUI _currentRecord;
    [SerializeField] private TextMeshProUGUI _recordHolder;

    [SerializeField]
    private TextMeshProUGUI _sliderValue;
    [SerializeField] private Slider _slider;
    private float _convertValue;
    private int realSoundValue;

    private void Start()
    {
        inputfield = transform.Find("input").GetComponent<TMP_InputField>();
        _playername.text = $"Enter name first !! ";
        _placeholderInside.text = "Enter your name...";
        _currentRecord.text = "" + PlayerPrefs.GetFloat("HighScore");
        _recordHolder.text = "" + PlayerPrefs.GetString("HighScoreHolder");
        _slider.value = 1f;
        _sliderValue.text = realSoundValue.ToString();
    }

    private void Update()
    {

        if (_convertValue <= 1)
        {
            _convertValue *= 100;
        }

        realSoundValue = (int)_convertValue;

        _sliderValue.text = realSoundValue.ToString();

    }

    public void SliderValueChange()
    {
        MenuBehaviour.SoundValue = _slider.value;
        _convertValue = _slider.value;
    }

    public void EnteredName()
    {
        if (inputfield.text == "")
        {
            _playername.text = "Please enter a name";
            Debug.Log("The string is empty");
        }

        if (inputfield.text != null && inputfield.text != string.Empty)
        {
            Debug.Log(inputfield.text);
            MenuBehaviour.Name = inputfield.text;
            _playername.text = $"Player: {MenuBehaviour.Name} ";
        }

    }

    public void RemoveName()
    {
        Debug.Log("no name entered?");
        MenuBehaviour.Name = null;
        _playername.text = $"Enter name first !! ";
        _placeholderInside.text = "Enter your name...";
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
