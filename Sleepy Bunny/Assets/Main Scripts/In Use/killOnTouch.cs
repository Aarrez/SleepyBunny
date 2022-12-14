using System.Collections;
using System.Collections.Generic;
using PlayerStM.BaseStates;
using UnityEngine;

public class killOnTouch : MonoBehaviour
{
    private GameMaster _gameMaster;

    private void Awake()
    {
        _gameMaster = FindObjectOfType<GameMaster>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player") { return; }

        collision.transform.position = _gameMaster.CurrentCheckpointPosition;
    }
}