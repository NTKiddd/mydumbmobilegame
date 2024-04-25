using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
    public Vector3 startPoint;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     if (Input.touches[0].phase == TouchPhase.Began)
        //     {
        //         startPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        //     }
        // }
    }
}
