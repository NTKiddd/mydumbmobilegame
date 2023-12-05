using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Collider2D _interactionCollider;
    
    [Space(15)]
    public string npcName;
    [SerializeField] private bool _interactable;

    public event Action OnInteract;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable = false;
        }
    }

    private void Update()
    {
        if (_interactable && InputHandler.Instance.touchCount > 0)
        {
            var touch = InputHandler.Instance.touches[0];
            
            if (touch.phase == TouchPhase.Began)
            {
                var touchPos = Camera.main!.ScreenToWorldPoint(touch.position);
                
                if (_interactionCollider.bounds.Contains((Vector2)touchPos))
                {
                    Debug.Log("Interact with NPC");
                    OnInteract?.Invoke();
                }
            }
        }
    }
}
