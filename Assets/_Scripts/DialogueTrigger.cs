using System;
using System.Collections;
using System.Collections.Generic;
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
        _ownedNPC = GetComponent<NPC>();
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
        if (!_isActive)
        {
            _isActive = true;
            
            dialogueManager.LoadDialogue(messages, _ownedNPC.npcName);
            _messageIndex = 0;
            StartCoroutine(dialogueManager.DisplayMessage(_messageIndex));
        }
        else if (_isActive && !dialogueManager.textScrolling)
        {
            _messageIndex++;
            StartCoroutine(dialogueManager.DisplayMessage(_messageIndex));

            if (_messageIndex == messages.Length)
            {
                _isActive = false;
            }
        }
    }
    
    private void StopDialogue()
    {
        _isActive = false;
        dialogueManager.CloseDialogue();
    }
}
