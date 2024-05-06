using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class InputHandler : Singleton<InputHandler>
{
    public delegate void TouchHandler(Touch[] touches, int touchCount);
    public event TouchHandler Touched;
    
    [field:SerializeField] public PlayerInput input { get; private set; }
    [field:SerializeField] public Vector2[] startPositions { get; private set; }
    
    // screen rect
    private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
    public Touch[] touches { get; private set; }
    public int touchCount { get; private set; }

    public override void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
    
    private void Start()
    {
        startPositions = new Vector2[5];
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector2(-Screen.width, -Screen.height)));
    }

    private void OnEnable()
    {
        //Debug.Log(this + " enabled");
        Touched += OnTouched;
    }

    private void OnDisable()
    {
        //Debug.Log(this + " disabled");
        Touched -= OnTouched;
    }

    private void Update()
    {
        touches = Input.touches;
        touchCount = touches.Length;

        if (touchCount > 0)
        {
            Touched?.Invoke(touches, touchCount);
            //Debug.Log(Input.touches[0].position);
            
            // Check if touch is inside screen
            // if (screenRect.Contains(Input.touches[0].position)) 
            // {
            //     Debug.Log("Inside");
            // }
            // else
            // {
            //     Debug.Log("Outside");
            // }
        }
    }

    private void OnTouched(Touch[] touches, int touchCount)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (touches[i].phase == TouchPhase.Began)
            {
                startPositions[i] = Camera.main.ScreenToWorldPoint(touches[i].position);
            }
        }
    }
}
