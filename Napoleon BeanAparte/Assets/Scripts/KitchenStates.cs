using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KitchenStates : MonoBehaviour
{
    public static List<Bean> BeansList = new List<Bean>();

    public static bool AreBeansWashed = false;

    public static bool IsCuttingDone = false;
    
    public static bool IsOrderCompleted = false;

    public static float Score;
    public enum CookingStation
    {
        Washing,
        Cutting,
        Cooking
    }

    public static CookingStation KitchenState;

    // Update is called once per frame
    void Update()
    {
        if (BeansList.Count < 0) //from the moment a bean is added to the list set this to true
        {
            AreBeansWashed = true;
        }
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
