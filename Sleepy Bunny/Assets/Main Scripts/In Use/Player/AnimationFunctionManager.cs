using System;
using UnityEngine;
using FMODUnity;
using UnityEngine.Events;

public class AnimationFunctionManager : MonoBehaviour
{
    //Used in SuperJumpPlayerState to play sound when jump
    public static Action OnJumpAction;

    public static event Action AnimationJumpEvent;

    public static Action Deathdone;

    [SerializeField] private EventReference _steps;

    [SerializeField] private EventReference _jump;

    [SerializeField] private EventReference _climb;

    [SerializeField] private EventReference _landingHard;

    [SerializeField] private EventReference _landingSoft;

    [SerializeField] private EventReference _dragingObject;

    [SerializeField] private EventReference _deathSound;

    // Subscribes the method JumpSound to an event
    // that happens when player jumps
    private void OnEnable()
    {
        OnJumpAction += JumpsSound;
    }

    private void OnDisable()
    {
        OnJumpAction -= JumpsSound;
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

    public void JumpsSound()
    {
        AnimationJumpEvent?.Invoke();
        if (_jump.IsNull) { return; }
        RuntimeManager.PlayOneShot(_jump);
    }

    public void landingHard()
    {
        if (_landingHard.IsNull) { return; }
        RuntimeManager.PlayOneShot(_landingHard);
    }

    public void landingSoft()
    {
        if (_landingSoft.IsNull) { return; }
        RuntimeManager.PlayOneShot(_landingSoft);
    }

    public void dragingObject()
    {
        if (_dragingObject.IsNull) { return; }
        RuntimeManager.PlayOneShot(_dragingObject);
    }

    private void DeathSound()
    {
        if (_deathSound.IsNull) { return; }
        RuntimeManager.PlayOneShot(_deathSound);
    }

    private void DeathAnimationDone()
    {
        Deathdone?.Invoke();
    }
}