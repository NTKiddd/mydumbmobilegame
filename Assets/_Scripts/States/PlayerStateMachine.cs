using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private Player _player;
    public PlayerState currentState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        _player = player;
    }

    public void SetState(PlayerState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState?.Exit();

        currentState = null;
        currentState = newState;
        currentState.Init(this, _player);
        currentState.Enter();
    }
}
