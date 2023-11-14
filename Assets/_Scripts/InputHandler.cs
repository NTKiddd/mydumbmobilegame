using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _input { get; set; }
    
    public delegate void TouchHandler(Touch[] touches);
    public event TouchHandler Touched;
    
    public Touch[] touches { get; private set; }
    public int touchCount { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        Debug.Log(this + " enabled");
    }

    private void OnDisable()
    {
        Debug.Log(this + " disabled");
    }

    private void Update()
    {
        touches = Input.touches;
        touchCount = touches.Length;

        if (touchCount > 0)
        {
            Touched?.Invoke(touches);
            //Debug.Log(Input.touches[0].position);
        }
    }
}
