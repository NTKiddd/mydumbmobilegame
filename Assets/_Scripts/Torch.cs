using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isBurning = true;
    private static readonly int IsBurning = Animator.StringToHash("isBurning");

    public void TurnOn()
    {
        _animator.SetBool(IsBurning, true);
        _isBurning = true;
    }
    
    public void TurnOff()
    {
        _animator.SetBool(IsBurning, false);
        _isBurning = false;
    }
}
