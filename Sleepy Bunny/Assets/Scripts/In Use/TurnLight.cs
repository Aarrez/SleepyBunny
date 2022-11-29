using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TurnLight : MonoBehaviour
{
    [SerializeField] private GameObject _lightSwitch;

    [SerializeField] private EventReference _LightSwitch;

    /// <summary>
    /// Used in PlayerStateMachine In the GrabClimbInteract Method <br></br>
    /// if this method has no overloads it reatruns the state of the light
    /// </summary>
    public void TheLight(bool letBeLight)
    {
        _lightSwitch.SetActive(letBeLight);

        RuntimeManager.PlayOneShot(_LightSwitch);
    }

    /// <summary>
    /// Used in PlayerStateMachine In the GrabClimbInteract Method <br></br>
    /// if this method has no overloads it reatruns the state of the light
    /// </summary>
    public bool TheLight()
    {
        return _lightSwitch.activeInHierarchy;
    }
}