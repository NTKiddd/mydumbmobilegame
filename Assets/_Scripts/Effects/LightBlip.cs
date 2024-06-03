using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightBlip : MonoBehaviour
{
    private Light2D _light;

    [SerializeField] private float _offTime;
    [SerializeField] private float _onTime;
    private float _originalIntensity;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Start()
    {
        _originalIntensity = _light.intensity;
        Invoke(nameof(Off), 1f);
    }

    private void Off()
    {
        _light.intensity = 0f;
        Invoke(nameof(On), _offTime);
    }

    private void On()
    {
        _light.intensity = _originalIntensity;
        Invoke(nameof(Off), _onTime);
    }
    
    
}
