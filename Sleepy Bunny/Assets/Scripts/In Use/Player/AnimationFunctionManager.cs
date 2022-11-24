using System;
using UnityEngine;
using FMODUnity;

public class AnimationFunctionManager
    : MonoBehaviour
{
    public static event Action LandAnimation, FallAnimation;

    [SerializeField] private EventReference _steps;
    [SerializeField] private EventReference _climb;
    [SerializeField] private EventReference _X;

    private bool playerIsMoving;

    [SerializeField]
    private float _walkingSpeed;

    public void FootstepSounds()
    {
        RuntimeManager.PlayOneShot(_steps);
    }

    public void SwitchFromFallingState()
    {
        LandAnimation?.Invoke();
    }

    public void FallingGrounded()
    {
        FallAnimation?.Invoke();
    }
}