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
    private GameObject Godlike;



    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pea":

                Common.SetActive(true);
                break;

            case "Navy":
                Uncommon.SetActive(true);
                break;

            case "Fava":
                Rare.SetActive(true);
                break;
            case "Anasazi":
                Epic.SetActive(true);
                break;
            case "French":
                Godlike.SetActive(true);
                break;


        }
        StartCoroutine(TwoSecondTimer(other));


    }
    IEnumerator TwoSecondTimer(Collider other)
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Code to execute after the timer completes
        Debug.Log("2 seconds have passed.");

        switch (other.gameObject.tag)
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
                Godlike.SetActive(false);
                break;

        }
    }
}