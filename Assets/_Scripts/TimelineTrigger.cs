using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _timeline;
    
    private void StartTimeline()
    {
        _timeline.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartTimeline();
        }
    }
}
