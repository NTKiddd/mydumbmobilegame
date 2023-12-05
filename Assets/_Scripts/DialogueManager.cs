using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text _nameDisplay;
    [SerializeField] private Text _messageDisplay;

    private string _currentMessage;
    private string[] _messages;

    public void StartDialogue(string[] messages, string npcName)
    {
        _nameDisplay.text = npcName;
        _messages = messages;
    }
}
