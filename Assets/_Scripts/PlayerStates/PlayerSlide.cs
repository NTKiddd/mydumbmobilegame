using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : PlayerGround
{
    private float _gravity;
    
    public override void Enter()
    {
        base.Enter();
        
        name = "Slide";
        Debug.Log(name);
        
        _gravity = player.rb.gravityScale;
        
        player.animator.Play("Slide");
    }

    public override void ExecuteUpdate()
    {
        if (input.touchCount > 0)
        {
            player.animator.Play("Hold");
            //Debug.Log("hold");
            player.rb.gravityScale = 0;
            player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        }
        else
        {
            player.animator.Play("Slide");
            //Debug.Log("slide");
            player.rb.gravityScale = _gravity;
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -player.wallSlideSpeed, float.MaxValue));
        }
        
        base.ExecuteUpdate();
    }

    public override void ExecuteFixedUpdate()
    {
        base.ExecuteFixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = player.gravity;
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
