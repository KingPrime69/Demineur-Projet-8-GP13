using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] UnityEvent _clicked;

    ClickDetector _mouse;

    private void Awake()
    {
        _mouse = FindObjectOfType<ClickDetector>();
        _mouse.Clicked += MouseOnClicked;
    }

    void MouseOnClicked()
    {
        Debug.Log("test");
        _clicked?.Invoke();
    }
}
