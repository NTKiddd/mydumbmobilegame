using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _pos1;
    [SerializeField] private GameObject _pos2;
    
    private Sequence _movingSequence;

    [SerializeField] private float _moveTime;
    [SerializeField] private float _waitTime;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //transform.position = _pos1.transform.position;
        
        _movingSequence = DOTween.Sequence();
        _movingSequence.Append(transform.DOMove(_pos1.transform.position, 0f))
            .Append(transform.DOMove(_pos2.transform.position, _moveTime))
            //.PrependInterval(_waitTime)
            .Pause()
            .Append(transform.DOMove(_pos1.transform.position, _moveTime));

        _movingSequence.OnComplete(() => _movingSequence.Restart());
    }
}
