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
    public event Action MiddleMouseClickedPress;
    public event Action MiddleMouseClickedRelease;

    void OnHover(InputValue value)
    {
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
    private void OnMiddleMouseClickPress(InputValue _)
    {
        MiddleMouseClickedPress?.Invoke();
    }
    private void OnMiddleMouseClickRelease(InputValue _)
    {
        MiddleMouseClickedPress?.Invoke();
    }
}
