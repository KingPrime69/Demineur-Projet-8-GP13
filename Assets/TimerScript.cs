using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timeValue = 0;
    public TMPro.TMP_Text timeText;
    // Update is called once per frame
    void Update()
    {
            timeValue += Time.deltaTime;
      

        DisplayTimeElapsed(timeValue);
    }

    void DisplayTimeElapsed(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
