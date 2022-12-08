using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class InteractObject : MonoBehaviour
{
    private Action _interacted;

    public Action Interacted => _interacted;

    [SerializeField] private EventReference _lightSwitch;

    private void OnEnable()
    {
        _interacted += InteractedWith;
    }

    private void OnDisable()
    {
        _interacted -= InteractedWith;
    }

    private void InteractedWith()
    {
        if (gameObject.tag == "Light")
        {
            if (GetComponentInChildren<Light>().isActiveAndEnabled)
            {
                GetComponentInChildren<Light>().enabled = false;
                if (_lightSwitch.IsNull) { return; }
                RuntimeManager.PlayOneShot(_lightSwitch);
            }
            else
            {
                GetComponentInChildren<Light>().enabled = true;
                if (_lightSwitch.IsNull) { return; }
                RuntimeManager.PlayOneShot(_lightSwitch);
            }
        }
        else if (gameObject.tag == "Door")
        {
            GetComponentInParent<Animator>().SetTrigger("OpenDoor");
        }
    }
}