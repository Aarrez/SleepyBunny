using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Checkpoints : MonoBehaviour
{
    private GameMaster _gameMaster;

    [SerializeField] private EventReference _lightSwitch;

    private void Awake()
    {
        _gameMaster = FindObjectOfType<GameMaster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateCheckpoint();

        GetComponent<BoxCollider>().enabled = false;
    }

    private void UpdateCheckpoint()
    {
        GetComponentInChildren<Light>().enabled = true;

        _gameMaster.CurrentCheckpointPosition = transform.position;

        if (_lightSwitch.IsNull) { return; }
        RuntimeManager.PlayOneShot(_lightSwitch);
    }
}