using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private string _string;
    
    



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
       if(KitchenStates.KitchenState == KitchenStates.CookingStation.Cutting)
        {
            Common.SetActive(false);
            Uncommon.SetActive(false);
            Rare.SetActive(false);
            Epic.SetActive(false);
            Legendary.SetActive(false);
        }
    }
}