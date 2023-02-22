using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class ClickDetector : MonoBehaviour
{
    public Vector2 WorldPosition { get; private set; }
    public event Action Clicked;

    private void OnAction(InputValue _)
    {
        Clicked?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
    }
}
