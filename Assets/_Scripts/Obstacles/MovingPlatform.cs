using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    [SerializeField] private GameObject endPosLocator;

    [SerializeField] private float _moveTime;
    [SerializeField] private float _waitTime;

    private void Start()
    {
        startPos = transform.position;
        endPos = endPosLocator.transform.position;

        MoveForward();
    }

    private void MoveForward()
    {
        transform.DOMove(endPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
            .OnComplete(MoveBackward);
    }
    
    private void MoveBackward()
    {
        transform.DOMove(startPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
            .OnComplete(MoveForward);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().IsGrounded(out GameObject groundedObject))
            {
                if (groundedObject == gameObject)
                {
                    other.transform.parent = transform;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
