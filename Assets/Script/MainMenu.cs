using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Difficulty.nbReveal = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    [SerializeField] TMP_Dropdown _dropDown;


    public void OnDropDownChange()
    {
        switch (_dropDown.value)
        {
            case 0:
                Difficulty.Size = new Vector2Int(10, 8);
                Difficulty.nbBombs = 10;
                break;
            case 1:
                Difficulty.Size = new Vector2Int(18, 14);
                Difficulty.nbBombs = 40;
                break;
            case 2:
                Difficulty.Size = new Vector2Int(24, 20);
                Difficulty.nbBombs = 99;
                break;
        }
        Difficulty.nbFlags = Difficulty.nbBombs;
    }
}

public static class Difficulty
{
    public static Vector2Int Size = new Vector2Int(10, 8);
    public static int nbBombs = 10;
    public static int nbReveal = 0;
    public static int nbFlags = nbBombs;

}

public static class WinLose
{
    public static string winLoseTitle = "Victory !";
    public static float timeValue = 0;
    public static bool isPlaying = false;
    public static bool isBombed = false;
}