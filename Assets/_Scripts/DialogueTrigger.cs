using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private NPC _ownedNPC;
    [SerializeField] private DialogueManager dialogueManager;
    
    [SerializeField] private string[] messages;

    private void Awake()
    {
        _ownedNPC = GetComponent<NPC>();
    }

    private void OnEnable()
    {
        _ownedNPC.OnInteract += StartDialogue;
    }

    private void StartDialogue()
    {
        dialogueManager.StartDialogue(messages, _ownedNPC.npcName);
    }
}
