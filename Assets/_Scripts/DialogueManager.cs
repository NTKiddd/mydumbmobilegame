using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Components
    [SerializeField] private TMP_Text _nameDisplay;
    [SerializeField] private TMP_Text _messageDisplay;
    [SerializeField] private GameObject _dialoguePanel;
    
    // Variables
    private string _currentMessage;
    private string[] _messages;
    public bool textScrolling { get; private set; }

    private void Start()
    {
        _dialoguePanel.SetActive(false);
    }

    public void LoadDialogue(string[] messages, string npcName)
    {
        _dialoguePanel.SetActive(true);
        
        _nameDisplay.text = npcName;
        _messages = messages;

        StartCoroutine(DisplayMessage(0));
    }
    
    public IEnumerator DisplayMessage(int index)
    {
        if (index < _messages.Length)
        {
            int _characterIndex = 0;
            
            // display messages' characters one by one overtime 
            textScrolling = true;
            for ( int i = 0; i < _messages[index].Length + 1; i++)
            {
                _messageDisplay.text = _messages[index].Substring(0, _characterIndex);
                _characterIndex++;
                yield return new WaitForSeconds(0.05f);
            }
            textScrolling = false;
        }
        else
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        _dialoguePanel.SetActive(false);
    }
}
