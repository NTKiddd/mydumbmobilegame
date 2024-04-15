using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerAir
{
    private Vector3 _force;

    public PlayerJump(Vector3 force)
    {
        _force = force;
    }

    public override void Enter()
    {
        base.Enter();
        
        name = "Jump";
        Debug.Log(name);

        //Debug.Log(_force);
        player.rb.velocity = Vector2.zero;
        player.rb.AddForce(_force, ForceMode2D.Impulse);
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
    }

    public override void ExecuteFixedUpdate()
    {
        base.ExecuteFixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();
        
        if (player.rb.velocity.y <= 0)
        {
            stateMachine.SetState(new PlayerFall());
        }
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
    }
}
