using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private PlayerStateMachine stateMachine { get; set; }

    public InputHandler input;
    public Camera cam { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }
    public Animator animator { get; private set; }
    public GameEvent onPlayerDie;

    [SerializeField] private Transform wallCheck;

    [SerializeField] public Trajectory trajectory;
    
    private Touch touch;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public LayerMask jumpableLayer;

    public float jumpForce;
    public float maxDrag;
    public float gravity;

    public float wallSlideSpeed;
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
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnPlayerDeath += Respawn;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnPlayerDeath -= Respawn;
    }

    private void Start()
    {
        stateMachine.SetState(new PlayerIdle());
        
        if (!Application.isEditor)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        else
        {
            Debug.Log("running in editor");
        }
    }

    private void Update()
    {
        stateMachine.currentState.ExecuteUpdate();
    }
    
    private void FixedUpdate()
    {
        stateMachine.currentState.ExecuteFixedUpdate();
    }

    public bool IsGrounded(out GameObject groundedObject)
    {
        var bounds = col.bounds;
        bool hit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, jumpableLayer);
        if (hit)
        {
            groundedObject = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0.1f, jumpableLayer).transform.gameObject;
        }
        else
            groundedObject = null;
        
        return hit;
    }

    public bool IsWalled()
    {
        var hit = Physics2D.OverlapCircle(wallCheck.position, 0.15f, jumpableLayer);
        
        if (hit)
        {
            return !hit.TryGetComponent(out PlatformEffector2D platform);
        }

        return false;
    }

    private void WallSlide()
    {
        // if (IsWalled() && !IsGrounded(out _))
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        // }
    }

    public void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Debug.Log(transform.localScale.x);
        facingRight = !facingRight;
    }
    
    // Respawn at last checkpoint
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    
    private IEnumerator RespawnCoroutine()
    {
        stateMachine.SetState(new PlayerDie());
        yield return new WaitForSeconds(0.5f);
        
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.5f);
        
        rb.isKinematic = false;

        transform.position = CheckpointManager.Instance.lastCheckpoint;
        
        var scale = transform.localScale;
        scale.x = 1;
        transform.localScale = scale;
        facingRight = true;
        stateMachine.SetState(new PlayerIdle());
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
