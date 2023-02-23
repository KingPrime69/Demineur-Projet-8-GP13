using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] UnityEvent _leftclicked;
    [SerializeField] UnityEvent _rightclicked;
    [SerializeField] UnityEvent _middleclickedpress;
    [SerializeField] UnityEvent _middleclickedrelease;
    BoxCollider2D _collider;
    ClickDetector _mouse;

    private void Awake()
    {
        _collider= GetComponent<BoxCollider2D>();
        _mouse = FindObjectOfType<ClickDetector>();
        _mouse.LeftMouseClicked += LeftMouseOnClicked;
        _mouse.RightMouseClicked += RightMouseOnClicked;
        _mouse.MiddleMouseClickedPress += MiddleMouseOnClickedPress;
        _mouse.MiddleMouseClickedRelease += MiddleMouseOnClickedRelease;
    }

    void LeftMouseOnClicked()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            _leftclicked?.Invoke();
        }
    }
    void RightMouseOnClicked()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            _rightclicked?.Invoke();
        }
    }
    void MiddleMouseOnClickedPress()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            _middleclickedpress?.Invoke();
        }
    }
    void MiddleMouseOnClickedRelease()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            _middleclickedrelease?.Invoke();
        }
    }
}
