using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerGround

{
    private float maxDrag;
    private LineRenderer _lineRenderer;

    public override void Enter()
    {
        base.Enter();

        name = "Idle";
        Debug.Log(name);
        
        player.canDash = true;
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
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
    }
}
