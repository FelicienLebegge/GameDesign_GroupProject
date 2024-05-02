using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KitchenStates : MonoBehaviour
{

    public List<Bean> ActiveBeansList = new List<Bean>();

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
