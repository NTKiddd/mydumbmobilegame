using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlash : BossState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("BossSlash");
        
        _boss.rb.velocity = Vector2.zero;
        _animator.Play("BossSlash");
    }

    public override void ExecuteUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            _boss.UpdatePos();
            _boss.SetState(new BossWalk());
        }
    }

    public override void ExecuteFixedUpdate()
    {
        
    }

    public override void Exit()
    {
        //_boss.UpdatePos();
        Debug.Log(_boss.transform.position);
    }
}
