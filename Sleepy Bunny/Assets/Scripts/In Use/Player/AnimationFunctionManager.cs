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

    private void OnEnable()
    {
        InputScript.doGrab += FootstepSounds;
    }

    public void FootstepSounds()
    {
        RuntimeManager.PlayOneShot(_steps);
        //FMOD.Studio.EventInstance _steps = FMODUnity.RuntimeManager.CreateInstance();
        //_steps.start();
        //_steps.release();
    }
}