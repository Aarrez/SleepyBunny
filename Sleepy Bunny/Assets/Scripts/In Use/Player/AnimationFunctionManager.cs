using System;
using UnityEngine;
using FMODUnity;
using UnityEngine.Events;

public class AnimationFunctionManager : MonoBehaviour
{
    //Used in SuperJumpPlayerState to play sound when jump
    public static Action<Vector3> OnJumpEvent;

    [SerializeField] private EventReference _steps;

    [SerializeField] private EventReference _jump;

    [SerializeField] private EventReference _climb;

    // Subscribes the method JumpSound to an event
    // that happens when player jumps
    private void OnEnable()
    {
        OnJumpEvent += JumpsSound;
    }

    private void OnDisable()
    {
        OnJumpEvent -= JumpsSound;
    }

    public void FootstepSounds()
    {
        RuntimeManager.PlayOneShot(_steps);
    }

    public void ClimbSound()
    {
        if (_climb.IsNull) { return; }
        RuntimeManager.PlayOneShot(_climb);
    }

    public void JumpsSound(Vector3 position)
    {
        if (_jump.IsNull) { return; }
        RuntimeManager.PlayOneShot(_jump, position);
    }
}