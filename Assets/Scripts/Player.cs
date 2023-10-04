using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float playerSpeed;
    private Touch touch;

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Debug.Log(touch.position.x);
            Debug.Log(touch.position.y);
        }
    }
}
