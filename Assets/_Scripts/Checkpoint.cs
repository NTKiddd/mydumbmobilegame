using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager _checkpointManager;
    private bool _isChecked;

    private void Awake()
    {
        _checkpointManager = CheckpointManager.Instance;
    }
    
    // store checkpoint position into CheckpointManager
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isChecked)
        {
            Debug.Log("Set checkpoint at " + transform.position);
            _checkpointManager.lastCheckpoint = transform.position;
            _isChecked = true;
        }
    }
}
