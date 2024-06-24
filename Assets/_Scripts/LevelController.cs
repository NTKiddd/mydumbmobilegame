using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _introTimeline;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _player;
    
    public event Action OnLevelStart; 

    void Start()
    {
        if (MenuTrigger.isMainMenu)
        {
            _camera.transform.position = new Vector3(0, 0, _camera.transform.position.z);
            _player.SetActive(false);
            _introTimeline.SetActive(true);
        }
    }
}
