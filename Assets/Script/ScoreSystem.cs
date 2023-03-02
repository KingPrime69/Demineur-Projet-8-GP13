using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    public int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        if (WinLose.isBombed) // If player has lost, 1min penality given
        {
            WinLose.timeValue += 60;
        }

        scoreValue = Mathf.FloorToInt((4013 * Difficulty.nbReveal) / WinLose.timeValue);
        scoreText.text = scoreValue.ToString();
    }
}
