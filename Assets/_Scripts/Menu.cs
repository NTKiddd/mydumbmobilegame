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
    [SerializeField] private GameObject _optionMenu;
    [SerializeField] private GameObject _mainMenu;
    
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

    public void ToggleMainMenu(bool value)
    {
        _mainMenu.SetActive(value);
        if (value == true)
        {
            _depthOfField.focusDistance.value = 3f;
        }
    }
    
    public void ToggleOptionMenu(bool value)
    {
        _optionMenu.SetActive(value);
        if (value == true)
        {
            _depthOfField.focusDistance.value = 1f;
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
    
    public void UpdateMusicVolume(Slider slider)
    {
        AudioManager.Instance.AdjustMusicVolume(slider.value / 10);
    }
    
    public void UpdateSFXVolume(Slider slider)
    {
        AudioManager.Instance.AdjustSFXVolume(slider.value / 10);
    }
}
