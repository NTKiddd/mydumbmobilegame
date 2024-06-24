using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
     private Bounds _bounds;
     [SerializeField] private bool _isStatic = true;
     
     private RoomController _roomController;

     private void Awake()
     {
         _bounds = GetComponent<CompositeCollider2D>().bounds;
         _roomController = GetComponentInParent<RoomController>();
     }

     public bool IsStatic()
     {
         return _isStatic;
     }
     
     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.transform.CompareTag("Player"))
         {
             _roomController.TurnOn();
         }
     }
     
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.transform.CompareTag("Player"))
         {
             _roomController.TurnOff();
         }
     }
    
}
