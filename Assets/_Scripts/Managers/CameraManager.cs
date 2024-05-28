using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Camera mainCamera;
    [field:SerializeField] public CinemachineVirtualCamera cineCam { get; set; }
    
    [SerializeField] private float _camSpeed;
    [field:SerializeField] public CameraBounds currentBounds { get; set; }
    public Coroutine cameraCoroutine { get; set; }
    
    public override void Awake()
    {
        base.Awake();
        
        mainCamera = Camera.main;
    }
    
    private void Start()
    {
        
    }

    public void SetCamPos(Vector3 pos)
    {
        cineCam.transform.position = pos;
        
        cineCam.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * _camSpeed);
    }

    public IEnumerator CameraMove(Vector3 targetPos)
    {
        //StopCoroutine(cameraCoroutine);
        float elapsedTime = 0;
        float waitTime = 2f;

        while (elapsedTime < waitTime)
        {
            cineCam.transform.position = Vector3.Lerp(cineCam.transform.position, targetPos, Time.deltaTime * _camSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
