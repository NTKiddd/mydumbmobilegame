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
             var hit = Physics2D.CircleCast(other.transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask("CameraBounds"));
                 
             if (Physics2D.CircleCast(other.transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask("CameraBounds")))
             {
                 Debug.Log(hit.transform.gameObject.name);
             }
             
             if (hit.transform.gameObject == gameObject)
             {
                 StartCoroutine(CameraManager.Instance.CameraMove(new Vector3(_bounds.center.x, _bounds.center.y, -10f)));
             }
         }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.transform.CompareTag("Player"))
         {
             
         }
     }

     public void MoveCamera()
     {
         if (CameraManager.Instance.currentBounds != this)
         {
             var coroutine = StartCoroutine(CameraManager.Instance.CameraMove(new Vector3(_bounds.center.x, _bounds.center.y, -10f)));
             CameraManager.Instance.currentBounds = this;
             CameraManager.Instance.cameraCoroutine = coroutine;
         }
     }
}
