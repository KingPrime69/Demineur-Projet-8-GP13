using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{

    //[SerializeField] AudioClip music;

    AudioSource _audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
