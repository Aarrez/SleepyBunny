using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Checkpoints : MonoBehaviour
{
    private GameMaster _gameMaster;

    [SerializeField] private EventReference _lightSwitch;

    [SerializeField] private Transform _spawnPoint;

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
        // GetComponentInChildren<Light>().enabled = true;
        // try
        // {
        //     Light[] lights = GetComponentsInChildren<Light>();
        //     for (int i = 0; i < lights.Length; i++)
        //     {
        //         lights[i].enabled = true;
        //     }
        // }
        // catch (System.Exception)
        // {
        //     GetComponentInChildren<Light>().enabled = true;
        // }

        _gameMaster.CurrentCheckpointPosition = _spawnPoint.position;

        if (_lightSwitch.IsNull) { return; }
        RuntimeManager.PlayOneShot(_lightSwitch);
    }
}