using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : IState
{
    protected Boss _boss;
    
    public void Init(Boss boss)
    {
        _boss = boss;
    }
    
    public virtual void Enter()
    {
        
    }

    public virtual void ExecuteUpdate()
    {
        Transition();
    }

    public virtual void ExecuteFixedUpdate()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public void Transition()
    {
        
    }
}
