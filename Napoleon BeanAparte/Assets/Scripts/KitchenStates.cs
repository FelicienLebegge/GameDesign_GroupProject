using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KitchenStates : MonoBehaviour
{

    public enum CookingStation
    {
        Washing,
        Cutting,
        Cooking
    }

    public CookingStation KitchenState;

    // Update is called once per frame
    void Update()
    {

    }

    public void SetKitchenState(CookingStation newSation)
    {
        KitchenState = newSation;

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
