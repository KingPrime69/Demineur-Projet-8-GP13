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
    public event Action LeftMouseClicked;
    public event Action RightMouseClicked;

    void OnHover(InputValue value)
    {
        Debug.Log("test");
        WorldPosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

    private void OnLeftMouseClick(InputValue _)
    {
        LeftMouseClicked?.Invoke();
    }

    private void OnRightMouseClick(InputValue _)
    {
        RightMouseClicked?.Invoke();
    }
}
