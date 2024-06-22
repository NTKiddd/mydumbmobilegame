using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeTime;
    private bool _isFading = false;

    private void OnEnable()
    {
        EventsManager.Instance.OnPlayerDeath += FadeEffect;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnPlayerDeath -= FadeEffect;
    }

    private void FadeEffect()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        Debug.Log("gooo");
        _image.enabled = true;
        _image.CrossFadeAlpha(0f, 0, false);
        
        // Fade in
        _image.CrossFadeAlpha(1.0f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);

        // Optional wait time between fades
        yield return new WaitForSeconds(0.3f);

        // Fade out
        _image.CrossFadeAlpha(0.0f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);
        
        _image.enabled = false;
    }
}
