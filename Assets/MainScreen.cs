using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.SwitchScene(GameManager.instance.SceneName.Game, 1);
    }
    public void GoToWeb()
    {
        Application.OpenURL("https://hotqueen.itch.io/pethome");
    }
}
