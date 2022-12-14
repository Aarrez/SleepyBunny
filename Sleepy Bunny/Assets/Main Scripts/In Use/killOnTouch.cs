using System.Collections;
using System.Collections.Generic;
using PlayerStM.BaseStates;
using UnityEngine;

public class killOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player") { return; }

        collision.gameObject.GetComponent<PlayerStateMachine>().PlayerDied();
    }
}