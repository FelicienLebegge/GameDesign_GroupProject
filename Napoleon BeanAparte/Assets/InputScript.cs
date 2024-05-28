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

    private void Start()
    {
        inputfield = transform.Find("input").GetComponent<TMP_InputField>();
        _playername.text = $"Enter name first !! ";
        _placeholderInside.text = "Enter your name...";
        _currentRecord.text = "Current HighScore: " + PlayerPrefs.GetFloat("HighScore");
        _recordHolder.text = "by: " + PlayerPrefs.GetString("HighScoreHolder");
    }

    public void EnteredName()
    {
        if (inputfield.text != null)
        {
            Debug.Log(inputfield.text);
            MenuBehaviour.Name = inputfield.text;
            _playername.text = $"Player:  {MenuBehaviour.Name} ";
        }

    }

    public void RemoveName()
    {
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
