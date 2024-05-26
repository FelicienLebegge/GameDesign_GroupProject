using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public static string Name;

    public void Play()
    {
        Debug.Log(Name);
        if (Name != null)
        {
            KitchenStates.Score = 0;
            KitchenStates.SpeedMultiplier = 1;
            Washer.IsDirtTooSMall = false;
            KitchenStates.IsCuttingDone = false;
            KitchenStates.AreBeansWashed = false;
            KitchenStates.IsOrderCompleted = false;
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
        SceneManager.LoadScene(0);
    }

}
