using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : PlayerState
{
    private LineRenderer _lineRenderer;
    bool touchStarted;

    public override void Enter()
    {
        base.Enter();
        
        _lineRenderer = player.GetComponent<LineRenderer>();
        
        if (TouchManager.Instance.startPoint != Vector3.zero)
        {
            startPoint = TouchManager.Instance.startPoint;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        if (touchStarted)
        {
            Debug.Log("go");
            TouchManager.Instance.startPoint = startPoint;
        }
        else
        {
            TouchManager.Instance.startPoint = Vector2.zero;
        }
        
    }
    
    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();

        if (input.touchCount > 0)
        {
            startPoint = input.startPositions[0];
            startPoint.z = 0;
            
            switch (input.touchCount)
            {
                case 1:
                {
                    Touch touch = input.touches[0];

                    if (touch.phase == TouchPhase.Began)
                    {
                        player.trajectory.ToggleTrajectory(true);
                        
                        _lineRenderer.positionCount = 1;
                        _lineRenderer.SetPosition(0, startPoint);
                        
                        //Debug.Log("Touch Pressed");
                        //Debug.Log(startPoint);
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        touchStarted = true;
                        
                        endPoint = cam.ScreenToWorldPoint(touch.position);
                        endPoint.z = 0;
                        var distance = Vector2.Distance(startPoint, endPoint);
                        var direction = (startPoint - endPoint).normalized;
                        var force = direction * (distance * player.jumpForce);
                        
                        _lineRenderer.positionCount = 2;
                        _lineRenderer.SetPosition(0, startPoint);
                        _lineRenderer.SetPosition(1, endPoint);
                        
                        // player.trajectory.ToggleTrajectory(true);
                        // player.trajectory.Plot(player.rb, player.transform.position, Vector3.ClampMagnitude(startPoint - endPoint, player.maxDrag) * player.jumpForce, 50);  
                        
                        //player.trajectory.poss = player.trajectory.SimulateTrajectory(player.transform.position, direction, player.jumpForce);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        endPoint = cam.ScreenToWorldPoint(touch.position);
                        endPoint.z = 0;
                        
                        Debug.Log(startPoint + ", " + endPoint);
                        player.trajectory.ToggleTrajectory(false);
                        
                        touchStarted = false;
                        _lineRenderer.positionCount = 0;
                        
                        if (Vector2.Distance(startPoint, endPoint) > 0.2f)
                        {
                            Vector3 force = startPoint - endPoint;
                            Vector3 clampedForce = Vector3.ClampMagnitude(force, player.maxDrag) * player.jumpForce;
                            
                            player.Flip();
                            stateMachine.SetState(new PlayerJump(clampedForce));
                        }
                    }
                    break;
                }
            } 
        }
        
        if (touchStarted)
        {
            player.trajectory.Plot(player.rb, player.transform.position, Vector3.ClampMagnitude(startPoint - endPoint, player.maxDrag) * player.jumpForce, 50);
        }
    }

    public override void Transition()
    {
        base.Transition();
        
        if (!player.IsWalled() && !player.IsGrounded(out _) && player.rb.velocity.y < 0)
        {
            stateMachine.SetState(new PlayerFall());
        }
    }

    protected void PassPoint()
    {
        
    }

    // protected override void OnTouched(Touch[] touches, int touchCount)
    // {
    //     base.OnTouched(touches, touchCount);
    //
    //     switch (touchCount)
    //     {
    //         case 1:
    //         {
    //             Touch touch = touches[0];
    //
    //             if (touch.phase == TouchPhase.Began)
    //             {
    //                 startPoint = cam.ScreenToWorldPoint(touch.position);
    //                 startPoint.z = 0;
    //                 _lineRenderer.positionCount = 1;
    //                 _lineRenderer.SetPosition(0, startPoint);
    //                 
    //                 touchStarted = true;
    //                 //Debug.Log("Touch Pressed");
    //                 //Debug.Log(startPoint);
    //             }
    //
    //             if (touch.phase == TouchPhase.Moved)
    //             {
    //                 endPoint = cam.ScreenToWorldPoint(touch.position);
    //                 endPoint.z = 0;
    //                 var distance = Vector2.Distance(startPoint, endPoint);
    //                 var direction = (startPoint - endPoint).normalized;
    //                 var force = direction * (distance * player.jumpForce);
    //                 
    //                 _lineRenderer.positionCount = 2;
    //                 _lineRenderer.SetPosition(1, endPoint);
    //                 
    //                 // player.trajectory.ToggleTrajectory(true);
    //                 // player.trajectory.Plot(player.rb, player.transform.position, Vector3.ClampMagnitude(startPoint - endPoint, player.maxDrag) * player.jumpForce, 50);  
    //                 
    //                 //player.trajectory.poss = player.trajectory.SimulateTrajectory(player.transform.position, direction, player.jumpForce);
    //             }
    //
    //             if (touch.phase == TouchPhase.Ended && touchStarted)
    //             {
    //                 endPoint = cam.ScreenToWorldPoint(touch.position);
    //                 endPoint.z = 0;
    //                 
    //                 Debug.Log(startPoint + ", " + endPoint);
    //                 player.trajectory.ToggleTrajectory(false);
    //
    //                 if (Vector2.Distance(startPoint, endPoint) > 0.2f)
    //                 {
    //                     Vector3 force = startPoint - endPoint;
    //                     Vector3 clampedForce = Vector3.ClampMagnitude(force, player.maxDrag) * player.jumpForce;
    //                     
    //                     player.Flip();
    //                     stateMachine.SetState(new PlayerJump(clampedForce));
    //                 }
    //                 
    //                 _lineRenderer.positionCount = 0;
    //                 
    //                 touchStarted = false;
    //             }
    //             break;
    //         }
    //     }
    // }
}
