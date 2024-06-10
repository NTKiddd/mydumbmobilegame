using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossState _currentState;
    
    [SerializeField] private int _health;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _health;
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
        _currentState.Init(this);
        _currentState.Enter();
    }
}
