using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Player _player;
    public List<Vector2> poss;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ToggleTrajectory(bool value)
    {
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

            if (Physics2D.CircleCast(calculatedPos, 0.5f, Vector2.zero))
            {
                break;
            }

            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(i, calculatedPos);
        }

        return lineRendererPoints;
    }
}
