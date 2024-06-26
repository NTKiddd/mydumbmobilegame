using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerDie : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        name = "Die";
        Debug.Log(name);
        
        Debug.Log(new Vector2(player.transform.localScale.x, 2f).normalized);
        player.rb.velocity = Vector2.zero;
        player.rb.gravityScale = 1f;
        player.rb.AddForce(new Vector2(-player.transform.localScale.x, 1.75f).normalized * 14f, ForceMode2D.Impulse);
        player.col.enabled = false;
        player.transform.DOScale(Vector3.zero, 1f);
        player.GetComponent<SpriteRenderer>().DOColor(Color.black, 1f);
        
        player.animator.Play("Die");
    }
    
    public override void ExecuteUpdate()
    {
        base.ExecuteUpdate();
    }

    public override void ExecuteFixedUpdate()
    {
        base.ExecuteFixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        player.col.enabled = true;
        player.rb.gravityScale = 10f;
    }

    public override void Transition()
    {
        base.Transition();
    }
}
