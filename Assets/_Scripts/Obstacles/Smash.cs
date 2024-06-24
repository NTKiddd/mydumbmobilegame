using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Smash : MonoBehaviour
{
    [SerializeField] private Collider2D _hitCollider;
    
    [Header("Settings")]
    [SerializeField] private float _startDelay;
    [SerializeField] private float _smashSpeed;
    [SerializeField] private float _retractSpeed;
    [SerializeField] private float _waitTime;
    
    private void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        _hitCollider.enabled = false;
        
        Invoke(nameof(Down), _startDelay);
    }

    private void Up()
    {
        _hitCollider.enabled = false;
        transform.DOScaleY(0f, _retractSpeed).OnComplete(Down).SetDelay(_waitTime);
    }

    private void Down()
    {
        _hitCollider.enabled = true;
        transform.DOScaleY(1, _smashSpeed).OnComplete(Up).SetDelay(_waitTime);
    }
    
    
}
