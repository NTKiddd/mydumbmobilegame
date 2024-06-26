using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private NPC _ownedNPC;
    [SerializeField] private DialogueManager dialogueManager;
    
    [SerializeField] private string[] messages;
    private int _messageIndex;
    private bool _isActive;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _ownedNPC.OnInteract += StartDialogue;
        _ownedNPC.OnUninteract += StopDialogue;
    }

    private void OnDisable()
    {
        _ownedNPC.OnInteract -= StartDialogue;
        _ownedNPC.OnUninteract -= StopDialogue;
    }

    private void StartDialogue()
    {
        // begin dialogue
        if (!_isActive)
        {
            _isActive = true;
            
            dialogueManager.LoadDialogue(messages);
            _messageIndex = 0;
            StartCoroutine(dialogueManager.DisplayMessage(_messageIndex));
        }
        // continue dialogue
        else if (_isActive && !dialogueManager.textScrolling)
        {
            _messageIndex++;
            StartCoroutine(dialogueManager.DisplayMessage(_messageIndex));

            if (_messageIndex == messages.Length)
            {
                _isActive = false;
                dialogueManager.CloseDialogue();
            }
        }
    }

    private void Update()
    {
        if (InputHandler.Instance.touchCount > 0 && _isActive && _messageIndex == messages.Length - 1)
        {
            Debug.Log("touch");
            StartDialogue();
        }
    }

    private void StopDialogue()
    {
        _isActive = false;
        dialogueManager.CloseDialogue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }
}
