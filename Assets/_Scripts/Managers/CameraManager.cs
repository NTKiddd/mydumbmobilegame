using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Camera mainCamera;
    [field:SerializeField] public CinemachineVirtualCamera cineCam { get; set; }
    [SerializeField] private CinemachineConfiner _confiner;
    [SerializeField] private GameObject _player;
    
    [SerializeField] private CameraBounds _currentBounds;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private bool isTransitioning;
    private Coroutine _cameraCoroutine;
    
    public override void Awake()
    {
        base.Awake();
        
        mainCamera = Camera.main;
    }

    private void Start()
    {
        //throw new NotImplementedException();
    }

    private IEnumerator OnCameraTransit(CameraBounds bounds)
    {
        //StopCoroutine(cameraCoroutine);
        float elapsedTime = 0;
        float waitTime = 2f;
        _confiner.m_BoundingShape2D = null;
        
        while (elapsedTime < waitTime)
        {
            var targetPos = new Vector3(bounds.transform.position.x, bounds.transform.position.y, -10f);
            cineCam.transform.position = Vector3.Lerp(cineCam.transform.position, targetPos, Time.deltaTime * _transitionSpeed);
            elapsedTime += Time.deltaTime;
            if (Vector2.Distance(cineCam.transform.position, targetPos) < 0.2f)
            {
                _confiner.m_BoundingShape2D = _currentBounds.GetComponent<Collider2D>();
                if (!bounds.IsStatic())
                {
                    cineCam.Follow = _player.transform;
                }
            }
            yield return null;
        }
    }
    
    public void TransitCamera(CameraBounds bounds)
    {
        if (_cameraCoroutine != null)
        {
            StopCoroutine(_cameraCoroutine);
        }
        _currentBounds = bounds;
        
        _cameraCoroutine = StartCoroutine(OnCameraTransit(bounds));
    }
    
    private IEnumerator OnShake(float strength, float duration)
    {
       var noise = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       noise.m_AmplitudeGain = strength;
       
       yield return new WaitForSeconds(duration);
       noise.m_AmplitudeGain = 0;
    }
    
    public void Shake(float strength, float duration)
    {
        StartCoroutine(OnShake(strength, duration));
    }
}
