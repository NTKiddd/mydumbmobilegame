using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class Player : MonoBehaviour
{
    private PlayerStateMachine stateMachine { get; set; }

    public InputHandler input;
    public Camera cam { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    [SerializeField] private Transform wallCheck;

    [SerializeField] private Trajectory trajectory;
    
    private Touch touch;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public LayerMask jumpableLayer;
    [SerializeField] private LayerMask slideableLayer;

    public float jumpForce;
    public float maxDrag;
    
    [SerializeField] private float wallSlideSpeed;
    public bool facingRight { get; private set; } = true;
    
    [Header("Dashing")]
    public float dashSpeed;
    public float dashTime;
    public bool canDash;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);

        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        stateMachine.SetState(new PlayerIdle());
    }

    private void Update()
    {
        stateMachine.currentState.ExecuteUpdate();
    }
    
    private void FixedUpdate()
    {
        stateMachine.currentState.ExecuteFixedUpdate();
    }

    // private void OnTouch(Touch[] touches, int touchCount)
    // {
    //     //touches = _input.touches;
    //     
    //     switch (touchCount)
    //     {
    //         case 1:
    //         {
    //             if (touches[0].phase == TouchPhase.Began)
    //             {
    //                 startPoint = cam.ScreenToWorldPoint(touches[0].position);
    //                 startPoint.z = 0;
    //                 //Debug.Log(startPoint);
    //
    //                 //trajectory.Show();
    //
    //                 Debug.Log("Touch Pressed");
    //             }
    //
    //             if (touches[0].phase == TouchPhase.Moved)
    //             {
    //                 trajectory.Show();
    //                 endPoint = cam.ScreenToWorldPoint(touches[0].position);
    //                 var distance = Vector2.Distance(startPoint, endPoint);
    //                 var direction = (startPoint - endPoint).normalized;
    //                 var force = distance * direction * jumpForce;
    //
    //                 trajectory.UpdateDots(transform.position, force);
    //             }
    //
    //             if (touches[0].phase == TouchPhase.Ended)
    //             {
    //                 //endPoint = cam.ScreenToWorldPoint(touch.position);
    //                 endPoint.z = 0;
    //                 //Debug.Log(endPoint);
    //                 trajectory.Hide();
    //                 Debug.Log("Touch Lifted/Released");
    //
    //                 if (IsGrounded() || IsWalled())
    //                 {
    //                     Vector3 force = startPoint - endPoint;
    //                     Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * jumpForce;
    //
    //                     if (force.x < 0 && facingRight || force.x > 0 && !facingRight)
    //                     {
    //                         facingRight = !facingRight;
    //                         Flip();
    //                     }
    //
    //                     rb.AddForce(clampedForce, ForceMode2D.Impulse);
    //                 }
    //             }
    //             break;
    //         }
    //
    //         case 2:
    //         {
    //             Touch firstTouch = touches[0];
    //             Touch secondTouch = touches[1];
    //
    //             //Debug.Log("Double Touch");
    //             
    //             if (firstTouch.phase == TouchPhase.Began && secondTouch.phase == TouchPhase.Began)
    //             {
    //                 startPoint = cam.ScreenToWorldPoint((firstTouch.position + secondTouch.position) / 2);
    //                 startPoint.z = 0;
    //             }
    //             
    //             if (firstTouch.phase == TouchPhase.Moved && secondTouch.phase == TouchPhase.Moved)
    //             {
    //                 endPoint = cam.ScreenToWorldPoint((firstTouch.position + secondTouch.position) / 2);
    //                 var distance = Vector2.Distance(startPoint, endPoint);
    //                 Debug.Log(distance);
    //
    //                 if (distance > 1f)
    //                 {
    //                     
    //                 }
    //                 
    //                 var direction = (endPoint - startPoint).normalized;
    //                 var force = distance * direction * jumpForce;
    //             }
    //             
    //             break;
    //         }
    //     }
    //
    //     WallSlide();
    // }

    public bool IsGrounded()
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
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    public void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
