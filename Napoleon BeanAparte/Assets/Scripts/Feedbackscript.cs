using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Feedbackscript : MonoBehaviour
{
    [SerializeField]
    private GameObject Common;
    [SerializeField]
    private GameObject Uncommon;
    [SerializeField]
    private GameObject Rare;
    [SerializeField]
    private GameObject Epic;
    [SerializeField]
    private GameObject Legendary;

    private GameObject[] _popups = new GameObject[0];

    private Vector3[] _positions = new Vector3[0];
    private Vector3[] _scales = new Vector3[0];

    private float[] _lerps = new float[0];

    private string _string;

    private void Start()
    {
        if (_popups.Length != 5)
        {
            AddToArray();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pea":
                Common.SetActive(true);
                _string = "Pea";
                break;
            case "Navy":
                Uncommon.SetActive(true);
                _string = "Navy";
                break;
            case "Fava":
                _string = "Fava";
                Rare.SetActive(true);
                break;
            case "Anasazi":
                _string = "Anasazi";
                Epic.SetActive(true);
                break;
            case "French":
                _string = "French";
                Legendary.SetActive(true);
                break;


        }
        StartCoroutine(TwoSecondTimer());


    }
    IEnumerator TwoSecondTimer()
    {
        yield return new WaitForSeconds(0.5f);
        switch (_string)
        {
            case "Pea":
                Common.SetActive(false);
                break;
            case "Navy":
                Uncommon.SetActive(false);
                break;
            case "Fava":
                Rare.SetActive(false);
                break;
            case "Anasazi":
                Epic.SetActive(false);
                break;
            case "French":
                Legendary.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _popups.Length; i++)
        {
            if (_popups[i].gameObject.active == true)
            {
                _popups[i].transform.position = Vector3.Lerp(_positions[i], new Vector3(50, Screen.height - 50, 0), _lerps[i]);
                _popups[i].transform.localScale = Vector3.Lerp(_scales[i], Vector3.zero, _lerps[i]);
                _lerps[i] += 1 * Time.deltaTime;
            }
            else
            {
                _lerps[i] = 0;
            }
        }

        if(KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            for (int i = 0; i < _lerps.Length; i++)
            {
                _lerps[i] = 0;
            }
            Common.SetActive(false);
            Uncommon.SetActive(false);
            Rare.SetActive(false);
            Epic.SetActive(false);
            Legendary.SetActive(false);
        }
    }

    private void AddToArray()
    {
        _popups = _popups.Append(Common).ToArray();
        _popups = _popups.Append(Uncommon).ToArray();
        _popups = _popups.Append(Rare).ToArray();
        _popups = _popups.Append(Epic).ToArray();
        _popups = _popups.Append(Legendary).ToArray();

        _positions = _positions.Append(Common.transform.position).ToArray();
        _positions = _positions.Append(Uncommon.transform.position).ToArray();
        _positions = _positions.Append(Rare.transform.position).ToArray();
        _positions = _positions.Append(Epic.transform.position).ToArray();
        _positions = _positions.Append(Legendary.transform.position).ToArray();

        _scales = _scales.Append(Common.transform.localScale).ToArray();
        _scales = _scales.Append(Uncommon.transform.localScale).ToArray();
        _scales = _scales.Append(Rare.transform.localScale).ToArray();
        _scales = _scales.Append(Epic.transform.localScale).ToArray();
        _scales = _scales.Append(Legendary.transform.localScale).ToArray();

        for (int i = 0; i < 5; i++)
        {
            _lerps = _lerps.Append(0).ToArray();
        }
    }
}