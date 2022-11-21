using System;
using UnityEngine;

public class AnimationFunctionManager
    : MonoBehaviour
{
    public static event Action LandAnimation;

    public void SwitchFromFallingState()
    {
        LandAnimation?.Invoke();
    }
}