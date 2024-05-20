using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("CameraBounds"))
        {
            other.transform.gameObject.GetComponent<CameraBounds>().MoveCamera();
        }
    }
}
