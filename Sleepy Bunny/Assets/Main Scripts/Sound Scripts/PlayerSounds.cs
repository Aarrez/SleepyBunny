using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;

    private void Awake()
    {
        if(!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }
    }

    public void PlayFootsteps()
    {
        if(footsteps.isValid())
        {
            //FMODUnity.RuntimeManager.AttachInstanceToGameObject(footsteps, transform);
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            footsteps.setParameterByName("Footsteps",0);
            footsteps.start();
        }
    }
}
