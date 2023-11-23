using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerState
{
    private float _dashSpeed => player.dashSpeed;
    private float _dashTime;
    private Vector2 _dashDir;
    
    private float _originalGravityScale;

    public PlayerDash(Vector2 direction)
    {
        _dashDir = direction;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        name = "Dash";
        Debug.Log(name);

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
