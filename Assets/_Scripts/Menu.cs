using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    
    
    public void Quit()
    {
        EditorApplication.ExitPlaymode();
    }
}
