using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] UnityEvent _leftclicked;
    [SerializeField] UnityEvent _rightclicked;
    BoxCollider2D _collider;
    ClickDetector _mouse;

    private void Awake()
    {
        _collider= GetComponent<BoxCollider2D>();
        _mouse = FindObjectOfType<ClickDetector>();
        _mouse.LeftMouseClicked += LeftMouseOnClicked;
        _mouse.RightMouseClicked += RightMouseOnClicked;
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
}
