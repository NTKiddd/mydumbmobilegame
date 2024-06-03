using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    
    public void AdjustSFXVolume(float value)
    {
        _sfxSource.volume = value;
    }
    
    public void AdjustMusicVolume(float value)
    {
        _musicSource.volume = value;
    }
}
