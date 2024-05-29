using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public static string Name;
    public static float SoundValue;
    [SerializeField] private TextMeshProUGUI _player;
    private float _pulseSpeed = 1.0f; 
    private float _minScale = 0.9f;   
    private float _maxScale = 1.5f;   
    private Color _startColor = new Color(0.5f, 0, 0); 
    private Color _endColor = Color.red;
    //THEfinalpush
    public void Play()
    {
        if (Name != null)
        {
            Debug.Log(Name);
            KitchenStates.KitchenState = KitchenStates.CookingStation.Washing;
            KitchenStates.Score = 0;
            KitchenStates.SpeedMultiplier = 1;
            Washer.IsDirtTooSMall = false;
            KitchenStates.IsCuttingDone = false;
            KitchenStates.AreBeansWashed = false;
            KitchenStates.IsOrderCompleted = false;
            KitchenStates.BeansList.Clear();
            SceneManager.LoadScene(1);
        }

        else
        {
            StartCoroutine(Pulse());
            StartCoroutine(Pulse());
            StartCoroutine(Pulse());
        }

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void About()
    {
        SceneManager.LoadScene(3);
    }

    public void Menu()
    {
        Name = null;
        KitchenStates.Score = 0;
        KitchenStates.SpeedMultiplier = 1;
        Washer.IsDirtTooSMall = false;
        KitchenStates.IsCuttingDone = false;
        KitchenStates.AreBeansWashed = false;
        KitchenStates.IsOrderCompleted = false;
        KitchenStates.BeansList.Clear();
        SceneManager.LoadScene(0);
    }
    IEnumerator Pulse()
    {

        yield return ScaleAndColorText(_minScale, _maxScale + 0.2f, _startColor, _endColor, _pulseSpeed);

        yield return ScaleAndColorText(_maxScale -0.2f, _minScale, _endColor, _startColor, _pulseSpeed);
    }
    IEnumerator ScaleAndColorText(float startScale, float endScale, Color startColor, Color endColor, float duration)
    {
        float time = 0;
        Vector3 initialScale = _player.transform.localScale * startScale;
        Vector3 targetScale = _player.transform.localScale * endScale;
        Color initialColor = startColor;
        Color targetColor = endColor;

        while (time < duration)
        {
            float t = time / duration;
            _player.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            _player.color = Color.Lerp(startColor, targetColor, t);
            time += Time.deltaTime/duration;
            yield return null;
        }

        _player.transform.localScale = targetScale;
        _player.color = initialColor;
    }

}
