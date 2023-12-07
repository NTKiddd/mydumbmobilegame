using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Vector2 lastCheckpoint;

    private void Start()
    {
        Debug.Log(this.gameObject.name);
    }
}
