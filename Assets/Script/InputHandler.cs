using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] UnityEvent _leftclicked;
    [SerializeField] UnityEvent _rightclicked;
    [SerializeField] UnityEvent _middleclickedpress;
    [SerializeField] UnityEvent _middleclickedrelease;
    [SerializeField] UnityEvent _hoverstart;
    [SerializeField] UnityEvent _hoverend;

    BoxCollider2D _collider;


    InputDetector _mouse;

    private void Awake()
    {
        _collider= GetComponent<BoxCollider2D>();
        _mouse = FindObjectOfType<InputDetector>();
        _mouse.LeftMouseClicked += LeftMouseOnClicked;
        _mouse.RightMouseClicked += RightMouseOnClicked;
        _mouse.MiddleMouseClickedPressed += MiddleMouseOnClickedPress;
        _mouse.MiddleMouseClickedReleased += MiddleMouseOnClickedRelease;
        _mouse.Hover += MouseHover;
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

    void MouseHover()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            _hoverstart.Invoke();
        }
        else if(!_collider.bounds.Contains(_mouse.WorldPosition))
        {            
            _hoverend.Invoke();
        }
    }
}
