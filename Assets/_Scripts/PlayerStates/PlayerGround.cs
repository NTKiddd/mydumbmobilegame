using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : PlayerState
{
    private LineRenderer _lineRenderer;
    bool touchStarted = false;

    public override void Enter()
    {
        base.Enter();
        
        _lineRenderer = player.GetComponent<LineRenderer>();
    }

    public override void Exit()
    {
        base.Exit();
        
        _lineRenderer.positionCount = 0;
    }

    public override void Transition()
    {
        base.Transition();
        
        if (!player.IsWalled() && !player.IsGrounded(out _) && player.rb.velocity.y < 0)
        {
            stateMachine.SetState(new PlayerFall());
        }
    }

    protected override void OnTouched(Touch[] touches, int touchCount)
    {
        base.OnTouched(touches, touchCount);

        switch (touchCount)
        {
            case 1:
            {
                Touch touch = touches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    startPoint = cam.ScreenToWorldPoint(touch.position);
                    startPoint.z = 0;
                    _lineRenderer.positionCount = 1;
                    _lineRenderer.SetPosition(0, startPoint);
                    
                    touchStarted = true;
                    //Debug.Log("Touch Pressed");
                    //Debug.Log(startPoint);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    endPoint = cam.ScreenToWorldPoint(touch.position);
                    endPoint.z = 0;
                    var distance = Vector2.Distance(startPoint, endPoint);
                    var direction = (startPoint - endPoint).normalized;
                    var force = direction * (distance * player.jumpForce);
                    
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPosition(1, endPoint);
                    
                    player.trajectory.ToggleTrajectory(true);
                    player.trajectory.Plot(player.rb, player.transform.position, Vector3.ClampMagnitude(startPoint - endPoint, player.maxDrag) * player.jumpForce, 50);  
                    
                    //player.trajectory.poss = player.trajectory.SimulateTrajectory(player.transform.position, direction, player.jumpForce);
                }

                if (touch.phase == TouchPhase.Ended && touchStarted)
                {
                    endPoint = cam.ScreenToWorldPoint(touch.position);
                    endPoint.z = 0;
                    
                    Debug.Log(startPoint + ", " + endPoint);
                    player.trajectory.ToggleTrajectory(false);

                    if (Vector2.Distance(startPoint, endPoint) > 0.2f)
                    {
                        Vector3 force = startPoint - endPoint;
                        Vector3 clampedForce = Vector3.ClampMagnitude(force, player.maxDrag) * player.jumpForce;
                        
                        player.Flip();
                        stateMachine.SetState(new PlayerJump(clampedForce));
                    }
                    
                    touchStarted = false;
                }
                break;
            }
        }
    }
}
