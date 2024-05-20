using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject _cam;
    
    [SerializeField] private float _parallaxFactor;
    private Vector2 _startPosition;

    private void Awake()
    {
        _cam = CameraManager.Instance.cineCam.gameObject;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float distance = _cam.transform.position.x * _parallaxFactor;
        transform.position = new Vector3(_startPosition.x + distance, transform.position.y, transform.position.z);
    }
}
