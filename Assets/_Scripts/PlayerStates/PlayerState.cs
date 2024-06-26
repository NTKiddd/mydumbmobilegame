using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected string name;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected InputHandler input;
    
    protected Camera cam;
    protected Vector3 startPoint;
    protected Vector3 endPoint;

    public void Init(PlayerStateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }
    
    public virtual void Enter()
    {
        input = player.input;
        input.Touched += OnTouched;

        cam = player.cam;
    }

    public virtual void ExecuteUpdate()
    {
        Transition();
        
        //Rotate player to face direction of movements
        if (player.rb.velocity != Vector2.zero)
        {
            if (player.rb.velocity.x < -0.1 && player.facingRight || player.rb.velocity.x > 0.1 && !player.facingRight)
            {
                player.Flip();
            }
        }
    }

    public virtual void ExecuteFixedUpdate()
    {
        var hit = Physics2D.CircleCast(player.transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask("CameraBounds"));
    }

    public virtual void Exit()
    {
        input.Touched -= OnTouched;
    }

    public virtual void Transition()
    {
        
    }
    
    protected virtual void OnTouched(Touch[] touches, int touchCount)
    {
        //Debug.Log("Touched");
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            stateMachine.SetState(new PlayerDie());
        }
    }
}
