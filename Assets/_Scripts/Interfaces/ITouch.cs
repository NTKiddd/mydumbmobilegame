using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouch
{
    void OnTouch(Touch[] touches, int touchCount);
}
