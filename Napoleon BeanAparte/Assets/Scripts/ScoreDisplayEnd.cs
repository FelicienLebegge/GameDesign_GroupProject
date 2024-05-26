using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayEnd : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endScore;

    [SerializeField]
    private TextMeshProUGUI _highscore;

    [SerializeField]
    private TextMeshProUGUI _name;

    private void Awake()
    {
        if (KitchenStates.Score == KitchenStates.Highscore)
        {
            _endScore.color = Color.green;
        }
        else
        {
            _endScore.color = Color.red;
        }

        _endScore.text = "You scored: " + KitchenStates.Score;
        _highscore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore");
        _name.text = "by: " + PlayerPrefs.GetString("HighScoreHolder");

    }
}
