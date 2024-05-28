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

    public static float Highscore;

    public static string HighscoreHolder;

    private float _totalTime = 180f; //3 minutes

    [SerializeField]
    private TextMeshProUGUI _scoreUI;

    [SerializeField]
    private TextMeshProUGUI _highscoreUI;

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

        _highscoreUI.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore", 0).ToString();

        _scoreUI.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        if (BeansList.Count < 0) //from the moment a bean is added to the list set this to true
        {
            AreBeansWashed = true;
        }

        if(BeansList.Count == 20)
        {
            Washer.IsDirtTooSMall = false;
        }

        _scoreUI.text = "Score: " + Score;
        UpdateTimer();
        UpdateHighScore();

        SpeedMultiplier = CalculateSpeedMultiplier();

        ResetHighscore();
    }

    private void ResetHighscore()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteKey("HighScore");
            _highscoreUI.text = "0";
        }
    }

    private void UpdateHighScore()
    {
        if (Score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", Score);
            Highscore = PlayerPrefs.GetFloat("HighScore", Score); //set equal to global float to display at the endscreen
            _highscoreUI.text = "HighScore: " + Score;

            PlayerPrefs.SetString("HighScoreHolder", MenuBehaviour.Name);
            HighscoreHolder = PlayerPrefs.GetString("HighScoreHolder");

            _scoreUI.color = Color.green;
        }
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
