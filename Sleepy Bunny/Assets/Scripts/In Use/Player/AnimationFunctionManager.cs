using System;
using UnityEngine;
using FMODUnity;

public class AnimationFunctionManager
    : MonoBehaviour
{
    public static event Action LandAnimation, FallAnimation;

    [SerializeField] private EventReference _inputSound;

    private bool playerIsMoving;

    [SerializeField]
    private float _walkingSpeed;

    public void FootstepSounds()
    {
        RuntimeManager.PlayOneShot(_inputSound);
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