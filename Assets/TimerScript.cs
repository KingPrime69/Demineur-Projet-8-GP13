using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private void Start()
    {
        WinLose.timeValue = 0;
        WinLose.isPlaying = false;
        WinLose.isBombed = false;
    }
    public TMPro.TMP_Text timeText;
    // Update is called once per frame
    void Update()
    {
        if (WinLose.isPlaying)
        { 
            WinLose.timeValue += Time.deltaTime;

            DisplayTimeElapsed(WinLose.timeValue);
        }
    }

    void DisplayTimeElapsed(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
