using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float _enableDuration;
    [SerializeField] private float _disableDuration;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        StartCoroutine(EnableDisableLaser());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Debug.Log("die");
            EventsManager.Instance.PlayerDie();
        }
    }

    private void EnableLaser()
    {
        col.enabled = true;
        spriteRenderer.enabled = true;
    }
    
    private void DisableLaser()
    {
        col.enabled = false;
        spriteRenderer.enabled = false;
    }
    
    private IEnumerator EnableDisableLaser()
    {
        while (true)
        {
            EnableLaser();
            yield return new WaitForSeconds(_enableDuration);
            DisableLaser();
            yield return new WaitForSeconds(_disableDuration);
        }
    }
}
