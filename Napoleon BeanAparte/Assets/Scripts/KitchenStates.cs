using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenStates : MonoBehaviour
{
    public static List<Bean> BeansList = new List<Bean>();

    public static bool AreBeansWashed = false;

    public static bool IsCuttingDone = false;
    
    public static bool IsOrderCompleted = false;

    public static bool IsTrashed = false;

    public static float Score;

    [SerializeField]
    private TextMeshProUGUI _scoreUI;

    [SerializeField]
    private TextMeshProUGUI _timerUI;

    private float _timeLeft;
    private bool _hasTimerStarted;

    public enum CookingStation
    {
        Washing,
        Cutting,
        Cooking
    }

    public static CookingStation KitchenState;

    private void Start()
    {
        _timeLeft = 180f; //3 minutes
        _hasTimerStarted = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (BeansList.Count < 0) //from the moment a bean is added to the list set this to true
        {
            AreBeansWashed = true;
        }

        _scoreUI.text = "Score: " + Score;

        if(_hasTimerStarted)
        {
            if(_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimer(_timeLeft);

            } else
            {
                Debug.Log("time's up");
                _timeLeft = 0;
                _hasTimerStarted = false;

                SceneManager.LoadScene(2); //load end screen
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 0.01f;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

        _timerUI.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void SetKitchenState(CookingStation newStation)
    {
        KitchenState = newStation;

        switch (KitchenState)
        {
            case CookingStation.Washing:
                break;
            case CookingStation.Cutting:

                break;
            case CookingStation.Cooking:
                break;
        }
    }

    
}
