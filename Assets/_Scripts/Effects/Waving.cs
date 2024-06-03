using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waving : MonoBehaviour
{
    [SerializeField] private float _intensity; 
    [SerializeField] private float _swingAngle;
    [SerializeField] private SwingDirection _swingDirection;

    private float initialRotationZ;
    private bool isSwingingRight = true;

    void Start()
    {
        initialRotationZ = transform.rotation.eulerAngles.z;
        
        if (_swingDirection == SwingDirection.Left)
        {
            isSwingingRight = false;
            _swingAngle *= -1;
        }
    }

    void FixedUpdate()
    {
        float swingAmount = Mathf.Sin(Time.time * _intensity) * _swingAngle;
        float currentRotationZ = initialRotationZ + swingAmount;
        
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
        
        if (Mathf.Abs(swingAmount) >= _swingAngle)
        {
            isSwingingRight = !isSwingingRight;
        }
    }
    
    private enum SwingDirection
    {
        Right,
        Left
    }
}
