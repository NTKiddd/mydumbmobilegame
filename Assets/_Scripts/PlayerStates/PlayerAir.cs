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
        
        if (input.touchCount == 1)
        {
            startPoint = cam.ScreenToWorldPoint((Vector2)input.startPositions[0]);
            startPoint.z = 0;
            Touch touch = input.touches[0];

            //Debug.Log("Double Touch");
        
            if (touch.phase == TouchPhase.Began)
            {
                startPoint = cam.ScreenToWorldPoint((Vector2)input.startPositions[0]);
                startPoint.z = 0;
                //Debug.Log("startPoint " + startPoint);
            }
        
            if (touch.phase == TouchPhase.Moved)
            {
                endPoint = cam.ScreenToWorldPoint(touch.position);
                endPoint.z = 0;
                var distance = Vector2.Distance(startPoint, endPoint);
                var direction = (endPoint - startPoint).normalized;
                var roundDir = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)).normalized;

                if (distance >= 2.5f && player.canDash)
                {
                    //endPoint = cam.ScreenToWorldPoint(touch.position);
                    stateMachine.SetState(new PlayerDash(roundDir));
                }
                
                //var force = direction * (distance * player.jumpForce);
            }
        }
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

    // protected override void OnTouched(Touch[] touches, int touchCount)
    // {
    //     base.OnTouched(touches, touchCount);
    //     
    //     if (touchCount == 1)
    //     {
    //         startPoint = input.startPositions[0];
    //         startPoint.z = 0;
    //         Touch touch = input.touches[0];
    //
    //         //Debug.Log("Double Touch");
    //     
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             startPoint = touch.position;
    //             startPoint.z = 0;
    //             //Debug.Log("startPoint " + startPoint);
    //         }
    //     
    //         if (touch.phase == TouchPhase.Moved)
    //         {
    //             endPoint = touch.position;
    //             endPoint.z = 0;
    //             var distance = Vector2.Distance(startPoint, endPoint);
    //             var direction = (endPoint - startPoint).normalized;
    //             var roundDir = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)).normalized;
    //             //Debug.Log(direction);
    //     
    //             if (distance >= 2f && player.canDash)
    //             {
    //                 stateMachine.SetState(new PlayerDash(roundDir));
    //             }
    //             
    //             //var force = direction * (distance * player.jumpForce);
    //         }
    //     }
    // }
}
