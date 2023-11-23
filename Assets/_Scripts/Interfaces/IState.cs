using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void ExecuteUpdate();
    void ExecuteFixedUpdate();
    void Exit();
    void Transition();
}
