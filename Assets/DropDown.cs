using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropDown;


    public void OnDropDownChange()
    {
        switch(dropDown.value)
        {
            case 0:
                //10 8 
                Difficulty.Size = new Vector2Int(10, 8);
                Difficulty.nbBombs = 10;
                break; 
            case 1:
                //18 14
                Difficulty.Size = new Vector2Int(18, 14);
                Difficulty.nbBombs = 40;
                break;
            case 2:
                //24 20
                Difficulty.Size = new Vector2Int(24, 20);
                Difficulty.nbBombs = 99;
                break;
        }
    }
}

public static class Difficulty
{
    public static Vector2Int Size = new Vector2Int(10,8);
    public static int nbBombs = 10;
}
