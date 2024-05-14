using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayEnd : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endScore;

    private void Awake()
    {
        _endScore.text = "You scored: " + KitchenStates.Score; 
    }
}
