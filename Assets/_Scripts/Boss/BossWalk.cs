using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : BossState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("BossWalk");
        _animator.Play("BossWalk");
    }

    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
        
        var direction = Mathf.Clamp((_player.transform.position.x - _boss.transform.position.x), -1, 1);
        
        if (direction > 0)
        {
            _boss.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _boss.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        _boss.rb.velocity = new Vector2(direction * _boss._moveSpeed, _boss.rb.velocity.y);
        
        
        if (Mathf.Abs(_player.transform.position.x - _boss.transform.position.x) < 5)
        {
            _boss.SetState(new BossSlash());
        }
    }

    public override void ExecuteFixedUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }
}
