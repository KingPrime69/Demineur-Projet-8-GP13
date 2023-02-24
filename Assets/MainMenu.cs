using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Switch to game");
        SceneManager.LoadScene(1);
        Debug.Log("Game Scene loaded");
        SceneManager.UnloadSceneAsync(0);
        Debug.Log("MainMenu unloaded");
    }

    public void QuitGame()
    {
        Debug.Log("Unity Quits");
        Application.Quit();
    }
}
