using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonGravityZone : MonoBehaviour
{
    [SerializeField] private float _modifiedGravityScale;
    private float _originalGravityScale;
    
    private Player _player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<Player>();
            _originalGravityScale = _player.rb.gravityScale;
            _player.rb.gravityScale = _modifiedGravityScale;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var _playerVelocity = _player.rb.velocity;
            _playerVelocity.y = Mathf.Clamp(_playerVelocity.y, -10, 10);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.rb.gravityScale = _originalGravityScale;
        }
    }
}
