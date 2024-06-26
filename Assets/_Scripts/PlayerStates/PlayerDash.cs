using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerState
{
    private float _dashSpeed => player.dashSpeed;
    private float _dashTime;
    private Vector2 _dashDir;
    
    private float _originalGravityScale;

    private SpriteRenderer _spriteRenderer;
    public PlayerDash(Vector2 direction)
    {
        _dashDir = direction;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        name = "Dash";
        Debug.Log(name);
        //Debug.Log(_dashDir);
        
        _spriteRenderer = player.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.cyan;
        
        _dashTime = player.dashTime;
        _originalGravityScale = player.rb.gravityScale;
        player.rb.gravityScale = 0f;
        
        player.canDash = false;
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();

        _dashTime -= Time.deltaTime;
        //Debug.Log(_dashTime);
        
        if (_dashTime >= 0)
        {
            player.rb.velocity = _dashDir * _dashSpeed;
        }
        else
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x / 2, player.rb.velocity.y);
            stateMachine.SetState(new PlayerFall());
        }
    }

    public override void ExecuteFixedUpdate()
    {
        base.ExecuteFixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = _originalGravityScale;
        _spriteRenderer.color = Color.white;
    }

    public override void Transition()
    {
        base.Transition();
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
    }
}
