using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Smash : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 startPos;
    private Vector2 endPos;
    [SerializeField] private GameObject endPosLocator;
    [SerializeField] private Collider2D smashCollider;

    [SerializeField] private float _moveTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _startDelay;
    [SerializeField] private bool _isSmashing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        smashCollider.enabled = false;
        startPos = transform.position;
        endPos = endPosLocator.transform.position;

        Invoke(nameof(MoveForward), _startDelay);
    }

    public void PlatformTrigger(Player player)
    {
        //player.transform.parent = transform;
    }

    public void PlatformCancel(Player player)
    {
        //player
    }

    private void MoveForward()
    {
        transform.DOMove(endPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
            .OnStart(() => smashCollider.enabled = true)
            .OnComplete(MoveBackward);

        // _rb.DOMove(endPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
        //     .OnComplete(MoveBackward);
    }
    
    private void MoveBackward()
    {
        transform.DOMove(startPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
            .OnStart(() => smashCollider.enabled = false)
            .OnComplete(MoveForward);
        
        // _rb.DOMove(startPos, _moveTime).SetEase(Ease.Linear).SetDelay(_waitTime)
        //     .OnComplete(MoveForward);
    }
    
    
}
