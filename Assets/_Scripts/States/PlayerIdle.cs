using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState

{
    private float maxDrag;

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

        switch (touchCount)
        {
            case 1:
            {
                if (touches[0].phase == TouchPhase.Began)
                {
                    startPoint = cam.ScreenToWorldPoint(touches[0].position);
                    startPoint.z = 0;
                    

                    //trajectory.Show();

                    Debug.Log("Touch Pressed");
                    //Debug.Log(startPoint);
                }

                if (touches[0].phase == TouchPhase.Moved)
                {
                    endPoint = cam.ScreenToWorldPoint(touches[0].position);
                    var distance = Vector2.Distance(startPoint, endPoint);
                    var direction = (startPoint - endPoint).normalized;
                    var force = direction * (distance * player.jumpForce);
                }

                if (touches[0].phase == TouchPhase.Ended)
                {
                    //endPoint = cam.ScreenToWorldPoint(touch.position);
                    endPoint.z = 0;
                    //trajectory.Hide();
                    Debug.Log("Touch Released");
                    //Debug.Log(endPoint);

                    Vector3 force = startPoint - endPoint;
                    Vector3 clampedForce = Vector3.ClampMagnitude(force, player.maxDrag) * player.jumpForce;

                    stateMachine.SetState(new PlayerJump(clampedForce));

                    // if (force.x < 0 && facingRight || force.x > 0 && !facingRight)
                    // {
                    //     facingRight = !facingRight;
                    //     Flip();
                    // }

                    //player.rb.AddForce(clampedForce, ForceMode2D.Impulse);
                }
                break;
            }

            // case 2:
            // {
            //     Touch firstTouch = touches[0];
            //     Touch secondTouch = touches[1];
            //
            //     if (firstTouch.phase == TouchPhase.Began && secondTouch.phase == TouchPhase.Began)
            //     {
            //         startPoint = cam.ScreenToWorldPoint((firstTouch.position + secondTouch.position) / 2);
            //         startPoint.z = 0;
            //     }
            //     
            //     if (firstTouch.phase == TouchPhase.Moved && secondTouch.phase == TouchPhase.Moved)
            //     {
            //         endPoint = cam.ScreenToWorldPoint((firstTouch.position + secondTouch.position) / 2);
            //         endPoint.z = 0;
            //         var distance = Vector2.Distance(startPoint, endPoint);
            //         Debug.Log(distance);
            //         var direction = (endPoint - startPoint).normalized;
            //
            //         if (distance > 1f)
            //         {
            //             stateMachine.SetState(new PlayerDash(direction));
            //         }
            //         
            //         var force = direction * (distance * player.jumpForce);
            //     }
            //     break;
            // }
        }
    }
}
