using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private VolumeProfile _volumeProfile;
    private DepthOfField _depthOfField;

    private void Start()
    {
        if (_volumeProfile.TryGet(out _depthOfField))
        {
            Debug.Log("Depth of Field found");
            _depthOfField.focusDistance.value = 3f;
        }
    }

    public void OnDisable()
    {
        _depthOfField.focusDistance.value = 3f;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        MenuTrigger.isMainMenu = true;
    }
    
    public void UpdateMusicVolume(Slider slider)
    {
        AudioManager.Instance.AdjustVolume(AudioManager.AudioType.Music, slider.value);
    }
    
    public void UpdateSFXVolume(Slider slider)
    {
        AudioManager.Instance.AdjustVolume(AudioManager.AudioType.SFX, slider.value);
    }
}
