using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class InputDetector : MonoBehaviour
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

    void OnLeftMouseClick(InputValue _)
    {
        LeftMouseClicked?.Invoke();
    }

    void OnRightMouseClick(InputValue _)
    {
        RightMouseClicked?.Invoke();
    }
    void OnMiddleMouseClickPress(InputValue _)
    {
        MiddleMouseClickedPressed?.Invoke();
    }
    void OnMiddleMouseClickRelease(InputValue _)
    {
        MiddleMouseClickedReleased?.Invoke();
    }
}
