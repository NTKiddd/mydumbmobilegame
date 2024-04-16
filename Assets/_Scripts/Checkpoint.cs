using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool _isChecked;

    // store checkpoint position into CheckpointManager
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isChecked)
        {
            Debug.Log("Set checkpoint at " + transform.position);
            CheckpointManager.Instance.lastCheckpoint = transform.position;
            _isChecked = true;
        }
    }
}
