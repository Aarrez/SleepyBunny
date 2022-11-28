using System;
using UnityEngine;
using FMODUnity;

public class AnimationFunctionManager
    : MonoBehaviour
{
    

    [SerializeField] private EventReference _steps;
    //[SerializeField] private EventReference _climb;
    //[SerializeField] private EventReference _X;

    private bool playerIsMoving;

    public void FootstepSounds()
    {
        

        RuntimeManager.PlayOneShot(_steps);

        //TRASH CODE NOT WORKY, DONT USE!
        //FMOD.Studio.EventInstance _steps = FMODUnity.RuntimeManager.CreateInstance();
        //_steps.start();
        //_steps.release();
        //SendMessage("play");
        //SendMessage("Stop");
    }
}