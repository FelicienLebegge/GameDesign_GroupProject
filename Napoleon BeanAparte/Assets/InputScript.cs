using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static TMP_InputField inputfield;
    public static Button _OK;
    [SerializeField] private TextMeshProUGUI _playername;

    private void Start()
    {
        inputfield = transform.Find("input").GetComponent<TMP_InputField>();
        Debug.Log("hallo im running");
    }

    private void Update()
    {
        _playername.text = $"Player:  {MenuBehaviour.Name} ";
    }

    public void EnteredName()
    {
        if (inputfield.text != null)
        {
            Debug.Log(inputfield.text);
            MenuBehaviour.Name = inputfield.text;
        }

    }

    public void RemoveName()
    {
        MenuBehaviour.Name = null;
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
