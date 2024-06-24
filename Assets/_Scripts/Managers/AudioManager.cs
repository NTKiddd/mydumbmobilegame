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
    
    public void AdjustVolume(AudioType type, float value)
    {
        switch (type)
        {
            case AudioType.Music:
                _musicSource.volume = value / 10;
                break;
            case AudioType.SFX:
                _sfxSource.volume = value / 10;
                break;
        }
    }
    
    public enum AudioType
    {
        Music,
        SFX
    }
}
