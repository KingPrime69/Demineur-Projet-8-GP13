using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class winLose : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] AudioClip win, loose;

    AudioSource _audioSrc;   // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        txt.text = WinLose.winLoseTitle;
        if(txt.text == "Victory !")
            _audioSrc.PlayOneShot(win);
        else
            _audioSrc.PlayOneShot(loose);
    }
}
