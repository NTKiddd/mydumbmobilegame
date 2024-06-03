using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
     private Bounds _bounds;
     [SerializeField] private bool _isStatic = true;

     private void Awake()
     {
         _bounds = GetComponent<CompositeCollider2D>().bounds;
     }

     public bool IsStatic()
     {
         return _isStatic;
     }
}
