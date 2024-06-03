using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsCheck : MonoBehaviour
{
    public GameEvent onPlayerEnterBounds;
    private GameObject _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CameraBounds bounds))
        {
            Debug.Log("enter");
            //other.transform.gameObject.GetComponent<CameraBounds>().MoveCamera();
            onPlayerEnterBounds.Raise(this, other);

            if (bounds.IsStatic())
            {
                CameraManager.Instance.cineCam.Follow = null;
            }
            CameraManager.Instance.TransitCamera(bounds);
        }
    }
}
