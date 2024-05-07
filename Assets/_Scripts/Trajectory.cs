using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Player _player;
    private List<Vector2> poss;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _player = GetComponentInParent<Player>();
    }

    public void ToggleTrajectory(bool value)
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.enabled = value;
    }

    private void Update()
    {
        if (InputHandler.Instance.touchCount > 0)
        {
            var touch = InputHandler.Instance.touches[0];
        }
    }

    public List<Vector2> SimulateTrajectory(Vector2 pos, Vector2 dir, float force)
    {
        List<Vector2> lineRendererPoints = new List<Vector2>();

        float maxDuration = 3f;
        float timeStep = 0.15f;
        int maxSteps = (int)(maxDuration / timeStep);

        Vector2 launchDirection = dir;
        Vector2 launchPosition = pos;

        var velocity = _player.jumpForce / _player.rb.mass * Time.fixedDeltaTime;

        for (int i = 0; i < maxSteps; i++)
        {
            Vector2 calculatedPos = launchPosition + launchDirection * (velocity * i * timeStep);
            calculatedPos.y += _player.rb.gravityScale / 2 * Mathf.Pow(i * timeStep, 2);
            
            lineRendererPoints.Add(calculatedPos);

            if (Physics2D.CircleCast(calculatedPos, 0.1f, Vector2.zero, 0f, LayerMask.NameToLayer("Ground")))
            {
                Debug.Log("trajectory end at " + calculatedPos);
                break;
            }

            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(i, calculatedPos);
        }

        return lineRendererPoints;
    }
    
    public Vector2[] Plot(Rigidbody2D rb, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * (_player.gravity * Mathf.Pow(timeStep, 2));
        
        float drag = 1f - timeStep * rb.drag;
        Vector2 moveStep = velocity * timeStep;
        
        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        _lineRenderer.positionCount = results.Length;
        for (int i = 0; i < results.Length; i++)
        {
            _lineRenderer.SetPosition(i, results[i]);
        }
        
        return results;
    }
}
