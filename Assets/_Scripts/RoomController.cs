using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Transform[] _objects;
    
    private void Awake()
    {
        _objects = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        TurnOff();
    }

    public void TurnOff()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            //_objects[i].SetActive(false);
            if (_objects[i].gameObject != this.gameObject && _objects[i].gameObject.layer != LayerMask.NameToLayer("CameraBounds"))
            {
                _objects[i].gameObject.SetActive(false);
            }
        }
    }
     
    public void TurnOn()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            //_objects[i].SetActive(true);
            _objects[i].gameObject.SetActive(true);
        }
    }
}
