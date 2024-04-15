using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir : PlayerState
{
    public override void Enter()
    {
        base.Enter();
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

        if (player.IsWalled())
        {
            stateMachine.SetState(new PlayerSlide());
        }
        
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
        
        if (touchCount == 1)
        {
            Touch touch = touches[0];

            //Debug.Log("Double Touch");
        
            if (touch.phase == TouchPhase.Began)
            {
                startPoint = touch.position;
                startPoint.z = 0;
                //Debug.Log("startPoint " + startPoint);
            }
        
            if (touch.phase == TouchPhase.Moved)
            {
                endPoint = touch.position;
                endPoint.z = 0;
                var distance = Vector2.Distance(startPoint, endPoint);

                var direction = (endPoint - startPoint).normalized;
                //Debug.Log(direction);
        
                if (distance >= 2f && player.canDash)
                {
                    stateMachine.SetState(new PlayerDash(direction));
                }
                
                //var force = direction * (distance * player.jumpForce);
            }
        }
    }
}
