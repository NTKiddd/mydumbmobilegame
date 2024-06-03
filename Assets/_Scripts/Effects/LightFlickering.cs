using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlickering : MonoBehaviour
{
    private Light2D _light;

    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float maxFrequency, minFrequency; 

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Start()
    {
        Invoke(nameof(Flicker), 1f);
    }

    private void Flicker()
    {
        _light.intensity = Random.Range(minIntensity, maxIntensity);
        Invoke(nameof(Flicker), Random.Range(minFrequency, maxFrequency));
    }
    
    
}
