using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossState _currentState;
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _pos;
    
    [SerializeField] private int _health;
    public float _moveSpeed;
    private int _currentHealth;

    [SerializeField] private Player player;
    private Vector2 _startPos;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        _startPos = transform.position;
    }

    private void Start()
    {
        _currentHealth = _health;
        SetState(new BossWalk());
    }
    
    private void OnEnable()
    {
        EventsManager.Instance.OnPlayerDeath += Reset;
        
        SetState(new BossWalk());
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnPlayerDeath -= Reset;
    }

    private void Update()
    {
        _currentState.ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.ExecuteFixedUpdate();
    }
    
    public void SetState(BossState newState)
    {
        if (_currentState != null)
        {
            if (_currentState.GetType() == newState.GetType())
            {
                return;
            }
        }

        _currentState?.Exit();

        _currentState = null;
        _currentState = newState;
        _currentState.Init(this, player, _animator);
        _currentState.Enter();
    }

    public void UpdatePos()
    {
        transform.position = _pos.transform.position;
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _currentHealth = _health;
        transform.position = _startPos;
        SetState(new BossWalk());
    }

    private void Reset()
    {
        StartCoroutine(ResetCoroutine());
    }
}
