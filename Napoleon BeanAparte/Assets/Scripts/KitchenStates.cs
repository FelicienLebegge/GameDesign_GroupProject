using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KitchenStates : MonoBehaviour
{
    public static List<GameObject> BeansList = new List<GameObject>();

<<<<<<< Updated upstream
=======
    public List<GameObject> BeansList = new List<GameObject>();


>>>>>>> Stashed changes
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
