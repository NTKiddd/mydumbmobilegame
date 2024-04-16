using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : PlayerGround
{
    public override void Enter()
    {
        base.Enter();
        
        name = "Slide";
        Debug.Log(name);
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
        
        player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -player.wallSlideSpeed, float.MaxValue));
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
        
        if (player.IsGrounded(out _))
        {
            stateMachine.SetState(new PlayerIdle());
        }
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
    }
}
