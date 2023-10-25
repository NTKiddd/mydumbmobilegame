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

    [SerializeField] private Transform wallCheck;

    [SerializeField] private Trajectory trajectory;
    
    private Touch touch;
    private Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField] private LayerMask jumpableLayer;
    [SerializeField] private LayerMask slideableLayer;

    [SerializeField] private float jumpForce;
    [SerializeField] private float maxDrag;
    [SerializeField] private float wallSlideSpeed;
    private bool canJump;
    private bool facingRight = true;
    private bool isSliding;
    
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
                
                    //trajectory.Show();
                
                Debug.Log("Touch Pressed");
            }
            
            if (touch.phase == TouchPhase.Moved)
            {
                trajectory.Show();
                endPoint = cam.ScreenToWorldPoint(touch.position);
                var distance = Vector2.Distance(startPoint, endPoint);
                var direction = (startPoint - endPoint).normalized;
                var force = distance * direction * jumpForce;
                
                trajectory.UpdateDots(transform.position, force);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                //endPoint = cam.ScreenToWorldPoint(touch.position);
                endPoint.z = 0;
                //Debug.Log(endPoint);
                trajectory.Hide();
                Debug.Log("Touch Lifted/Released");
                
                if (IsGrounded() || IsWalled())
                {
                    Vector3 force = startPoint - endPoint;
                    Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * jumpForce;

                    if (force.x < 0 && facingRight || force.x > 0 && !facingRight)
                    {
                        facingRight = !facingRight;
                        Flip();
                    }

                    rb.AddForce(clampedForce, ForceMode2D.Impulse);
                }
            }
        }
        
        WallSlide();
    }

    private bool IsGrounded()
    {
        var bounds = col.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, jumpableLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, jumpableLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded())
        {
            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isSliding = false;
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
