using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : IState
{
    protected Boss _boss;
    protected Player _player;
    protected Animator _animator;

    protected bool _isAttacking;
    
    public void Init(Boss boss, Player player, Animator animator)
    {
        _boss = boss;
        _player = player;
        _animator = animator;
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
