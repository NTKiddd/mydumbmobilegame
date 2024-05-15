using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera _cineCam;
    
    [SerializeField] private float _camSpeed;
    [field:SerializeField] public CameraBounds currentBounds { get; set; }
    [field:SerializeField] public CameraBounds bufferBounds { get; set; }
    public override void Awake()
    {
        base.Awake();
        
        mainCamera = Camera.main;
    }

    public void SetCamPos(Vector3 pos)
    {
        _cineCam.transform.position = pos;
        
        _cineCam.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * _camSpeed);
    }

    public IEnumerator CameraMove(Vector3 targetPos)
    {
        StopAllCoroutines();
        float elapsedTime = 0;
        float waitTime = 3f;
        
        while (elapsedTime < waitTime)
        {
            _cineCam.transform.position = Vector3.Lerp(_cineCam.transform.position, targetPos, Time.deltaTime * _camSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
