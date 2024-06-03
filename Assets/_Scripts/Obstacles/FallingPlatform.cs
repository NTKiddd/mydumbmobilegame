using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _col;
    
    [SerializeField] private float _timeToFall;
    [SerializeField] private float _timeToRespawn;
    private IEnumerator _wait;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnPlayerDeath += Respawn;
    }

    private void OnDisable()
    {
        //EventsManager.Instance.OnPlayerDeath -= Respawn;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _spriteRenderer.transform.DOShakePosition(_timeToFall, 0.1f, 10, 80f, false, false);
            StartCoroutine(WaitAndExecute(Fall, _timeToFall));
        }
    }

    private IEnumerator WaitAndExecute(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
    
    private void Fall()
    {
        _spriteRenderer.enabled = false;
        _col.enabled = false;
        StartCoroutine(WaitAndExecute(Respawn, _timeToRespawn));
    }
    
    private void Respawn()
    {
        _spriteRenderer.enabled = true;
        _col.enabled = true;
        StopAllCoroutines();
    }
}
