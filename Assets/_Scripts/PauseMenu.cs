using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, ITouch
{
    [SerializeField] private GameObject _pauseMenu;
    private InputHandler _input;

    private void Awake()
    {
        _input = InputHandler.Instance;
    }

    private void OnEnable()
    {
        _input.Touched += OnTouch;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        }
        else
        {
            Application.Quit();
        }
    }

    public void OnTouch(Touch[] touches, int touchCount)
    {
        if (touchCount == 2)
        {
            Debug.Log(touches[0].tapCount);
            Debug.Log(touches[1].tapCount);
            
            if (touches[0].tapCount == 2 || touches[1].tapCount == 2)
            {
                Pause();
            }
        }
    }
}
