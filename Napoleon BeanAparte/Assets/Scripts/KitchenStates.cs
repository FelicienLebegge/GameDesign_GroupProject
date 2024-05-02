using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KitchenStates : MonoBehaviour
{

<<<<<<< HEAD
=======
    public List<GameObject> BeansList = new List<GameObject>();

>>>>>>> parent of 72882cf (ListManager addition)
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
