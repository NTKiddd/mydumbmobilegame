using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _col;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    private void Deactivate()
    {
        _spriteRenderer.enabled = false;
        _col.enabled = false;
    }
}
