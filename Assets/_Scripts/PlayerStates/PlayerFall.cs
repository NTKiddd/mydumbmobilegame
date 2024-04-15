using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerAir
{
    public override void Enter()
    {
        base.Enter();
        
        name = "Fall";
        Debug.Log(name);
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
