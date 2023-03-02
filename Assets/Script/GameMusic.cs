using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{


    AudioSource _audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.Play();
    }
}
