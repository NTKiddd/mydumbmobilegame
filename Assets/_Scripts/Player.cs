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

    public void Respawn()
    {
        transform.position = CheckpointManager.Instance.lastCheckpoint;
        
        rb.velocity = Vector2.zero;
        
        var scale = transform.localScale;
        scale.x = 1;
        transform.localScale = scale;
    }
}
