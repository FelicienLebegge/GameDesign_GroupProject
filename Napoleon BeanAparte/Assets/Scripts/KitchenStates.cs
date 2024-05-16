using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private float _totalTime = 180f; //3 minutes

    [SerializeField]
    private TextMeshProUGUI _scoreUI;

    [SerializeField]
    private TextMeshProUGUI _timerUI;

    private float _timeLeft;
    private bool _hasTimerStarted;

    public static float SpeedMultiplier;

    public enum CookingStation
    {
        Washing,
        Cutting,
        Cooking
    }

    public static CookingStation KitchenState;

    private void Start()
    {
        _timeLeft = _totalTime; 
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
        UpdateTimer();

        SpeedMultiplier = CalculateSpeedMultiplier();

        Debug.Log(SpeedMultiplier);
    }

    private void UpdateTimer()
    {
        if (_hasTimerStarted)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimer(_timeLeft);

            }
            else
            {
                Debug.Log("time's up");
                _timeLeft = 0;
                _hasTimerStarted = false;

                SceneManager.LoadScene(2); //load end screen
            }

            if (_timeLeft < 60)
            {
                _timerUI.color = Color.red;

                if ((int)_timeLeft % 2 == 0)
                {
                    _timerUI.color = Color.white;
                }
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

    private float CalculateSpeedMultiplier()
    {
        float timePercentage = _timeLeft / _totalTime;


        float speedMultiplier = Mathf.Lerp(1f, 2f, 1 - timePercentage);

        return speedMultiplier;
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
