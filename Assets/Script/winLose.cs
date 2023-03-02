using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class winLose : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txt;
    [SerializeField] AudioClip _win, _loose;

    AudioSource _audioSrc;   
    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        _txt.text = WinLose.winLoseTitle;
        if(_txt.text == "Victory !")
            _audioSrc.PlayOneShot(_win);
        else
            _audioSrc.PlayOneShot(_loose);
    }
}
