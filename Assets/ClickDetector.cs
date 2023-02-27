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
    public event Action MiddleMouseClickedPressed;
    public event Action MiddleMouseClickedReleased;
    public event Action Hover;
    void OnHover(InputValue value)
    {
        WorldPosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        Hover.Invoke();
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
        MiddleMouseClickedPressed?.Invoke();
    }
    private void OnMiddleMouseClickRelease(InputValue _)
    {
        MiddleMouseClickedReleased?.Invoke();
    }
}
