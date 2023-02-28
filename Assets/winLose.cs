using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class winLose : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = WinLose.winLoseTitle;
    }
}
