using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : PlayerState
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

        if (player.IsGrounded())
        {
            stateMachine.SetState(new PlayerIdle());
        }
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);
        
        if (touchCount == 2)
        {
            Touch firstTouch = touches[0];
            Touch secondTouch = touches[1];
        
            //Debug.Log("Double Touch");
        
            if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
            {
                startPoint = (firstTouch.position + secondTouch.position) / 2;
                //startPoint.z = 0;
                Debug.Log("startPoint " + startPoint);
            }
        
            if (firstTouch.phase == TouchPhase.Moved && secondTouch.phase == TouchPhase.Moved)
            {
                endPoint = ((firstTouch.position + secondTouch.position) / 2);
                //endPoint.z = 0;
                var distance = Vector2.Distance(startPoint, endPoint);

                var direction = (endPoint - startPoint).normalized;
                Debug.Log(direction);
        
                if (distance >= 1f && player.canDash)
                {
                    stateMachine.SetState(new PlayerDash(direction));
                }
                
                //var force = direction * (distance * player.jumpForce);
            }
        }
    }
}
