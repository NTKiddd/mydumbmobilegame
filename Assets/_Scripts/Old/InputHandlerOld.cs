using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputHandlerOld : MonoBehaviour
{
    private PlayerInput _input { get; set; }
    
    public TouchState touch { get; private set; } 
    public TouchState[] touches { get; private set; }
    
    public int touchCount { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        _input.actions["Jump"].performed += OnTouch;
        Debug.Log("enabled");
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
        _input.actions["Jump"].performed -= OnTouch;
        Debug.Log("disabled");
    }
    
    private void OnTouch(InputAction.CallbackContext context)
    {
        touch = context.ReadValue<TouchState>();
    }

    private void Update()
    {
        touchCount = Input.touchCount;
        Debug.Log(touch.position);
    }
}
