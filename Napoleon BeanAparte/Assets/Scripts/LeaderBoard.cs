using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private Transform _entryContainer;

    [SerializeField]
    private Transform _entryTemplate;

    private static List<HighScoreEntry> _highScoreEntryList = new List<HighScoreEntry>();
    private List<Transform> _highScoreEntryTransformList;

    private void Awake()
    {
        AddHighScoreEntry((int)KitchenStates.Score, MenuBehaviour.Name);


        _entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("leaderboard");

        Highscores highscores = new Highscores();

        if (!string.IsNullOrEmpty(jsonString))
        {
            highscores = JsonUtility.FromJson<Highscores>(jsonString);

                _highScoreEntryList = highscores.HighScoreEntryList;
            
        }

        // Sort the high score entries and only keep the top 10
        _highScoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));
        if (_highScoreEntryList.Count > 10)
        {
            _highScoreEntryList = _highScoreEntryList.GetRange(0, 10);
        }


        _highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in highscores.HighScoreEntryList)
        {
            CreateHighScoreEntryTransform(highscoreEntry, _entryContainer, _highScoreEntryTransformList);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            ResetLeaderboard();
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {

        float templateHeight = 55f;

        Transform entryTransform = Instantiate(_entryTemplate, container); //set the template at the container transform
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default: rankString = rank + "TH"; break; //Add th after the rank on default, exeption on 1,2 and 3
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("RankText").GetComponent<TextMeshProUGUI>().text = rankString; //add the generated string to the template


        int score = highScoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("PlayerText").GetComponent<TextMeshProUGUI>().text = name;

        entryTransform.Find("BG").gameObject.SetActive(rank % 2 == 1); //set active if rank is uneven

        if (rank == 1)
        {
            entryTransform.Find("RankText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("PlayerText").GetComponent<TextMeshProUGUI>().color = Color.green;
        }

        transformList.Add(entryTransform);
    }


    public static void AddHighScoreEntry(int score, string name)
    {
        //create Entry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        //initialize highscores object
        Highscores highscores = new Highscores();

        //load saved highscores
        string jsonString = PlayerPrefs.GetString("leaderboard");
        if (!string.IsNullOrEmpty(jsonString))
        {
            highscores = JsonUtility.FromJson<Highscores>(jsonString);

                _highScoreEntryList = highscores.HighScoreEntryList;
            
        }

        

        //add entries list
        highscores.HighScoreEntryList.Add(highScoreEntry);

        //update and save highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("leaderboard", json);
        PlayerPrefs.Save();
    }

    public void ResetLeaderboard()
    {
        // Clear PlayerPrefs entry
        PlayerPrefs.DeleteKey("leaderboard");

        // Clear the high score entry list
        _highScoreEntryList.Clear();

        // Destroy all instantiated high score entry transforms
        foreach (Transform entryTransform in _highScoreEntryTransformList)
        {
            Destroy(entryTransform.gameObject);
        }

        _highScoreEntryTransformList.Clear();

        // Save
        Highscores highscores = new Highscores { HighScoreEntryList = new List<HighScoreEntry>() };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("leaderboard", json);
        PlayerPrefs.Save();
    }


    private class Highscores
    {
        public List<HighScoreEntry> HighScoreEntryList;
    }


    [System.Serializable]
    private class HighScoreEntry //= single highscore entry
    {
        public int score;
        public string name;
    }
}
