using System.Collections.Generic;
using UnityEngine;

public class KitchenStates : MonoBehaviour
{

    public List<GameObject> BeansList = new List<GameObject>();

    public List<GameObject> WashedBeansList = new List<GameObject>();

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
