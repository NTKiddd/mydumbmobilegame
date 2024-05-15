using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
     [SerializeField] private CameraManager _cameraController;
     private Bounds _bounds;

     private void Awake()
     {
         _bounds = GetComponent<CompositeCollider2D>().bounds;
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.transform.CompareTag("Player"))
         {
             if (CameraManager.Instance.currentBounds == null)
             {
                 StartCoroutine(CameraManager.Instance.CameraMove(new Vector3(_bounds.center.x, _bounds.center.y, -10)));
                 CameraManager.Instance.currentBounds = this;
             }
             else
             {
                 if (CameraManager.Instance.currentBounds != this)
                 {
                     CameraManager.Instance.bufferBounds = this;
                 }
             }
         }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.transform.CompareTag("Player") && CameraManager.Instance.currentBounds == this)
         {
             CameraManager.Instance.currentBounds = null;
             
             if (CameraManager.Instance.bufferBounds != null)
             {
                 CameraManager.Instance.currentBounds = CameraManager.Instance.bufferBounds;
                 CameraManager.Instance.bufferBounds = null;
                 StartCoroutine(CameraManager.Instance.CameraMove(new Vector3(CameraManager.Instance.currentBounds._bounds.center.x, CameraManager.Instance.currentBounds._bounds.center.y, -10)));
             }
         }
     }
}
