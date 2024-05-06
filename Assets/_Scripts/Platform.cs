using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public abstract void PlatformTrigger(Player player);
    public abstract void PlatformCancel(Player player);

}
