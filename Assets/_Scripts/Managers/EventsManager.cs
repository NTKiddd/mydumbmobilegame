using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Singleton<EventsManager>
{
    public event Action OnPlayerDeath;
    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
}
