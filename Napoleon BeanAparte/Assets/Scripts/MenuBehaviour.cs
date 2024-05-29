using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public static string Name;
    public static float SoundValue;

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

}
