using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] UnityEvent _clicked;
    BoxCollider2D _collider;
    ClickDetector _mouse;

    private void Awake()
    {
        _collider= GetComponent<BoxCollider2D>();
        _mouse = FindObjectOfType<ClickDetector>();
        _mouse.Clicked += MouseOnClicked;
    }

    void MouseOnClicked()
    {
        if (_collider.bounds.Contains(_mouse.WorldPosition))
        {
            Debug.Log("test");
            _clicked?.Invoke();
        }
    }
}
