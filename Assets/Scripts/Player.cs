using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class Player : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private Collider2D col;
    
    private Touch touch;
    private Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField] private LayerMask jumpableLayer;

    [SerializeField] private float jumpForce;
    [SerializeField] private float maxDrag;
    private bool canJump;
    
    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                startPoint = cam.ScreenToWorldPoint(touch.position);
                startPoint.z = 0;
                //Debug.Log(startPoint);
                Debug.Log("Touch Pressed");
            }
            
            // if (touch.phase == TouchPhase.Moved)
            // {
            //     //Debug.Log(endPoint);
            //     Debug.Log("Touch Dragged");
            // }

            if (touch.phase == TouchPhase.Ended)
            {
                endPoint = cam.ScreenToWorldPoint(touch.position);
                endPoint.z = 0;
                //Debug.Log(endPoint);
                Debug.Log("Touch Lifted/Released");
                
                if (IsGrounded())
                {
                    Vector3 force = startPoint - endPoint;
                    Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * jumpForce;

                    rb.AddForce(clampedForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private bool IsGrounded()
    {
        var bounds = col.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, jumpableLayer);
    }
}
