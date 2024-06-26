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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartTimeline();
        }
    }
}
