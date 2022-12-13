using System.Collections;
using System.Collections.Generic;
using PlayerStM.BaseStates;
using UnityEngine;

public class LockPlayerInPlace : MonoBehaviour
{
    private Collider _player;

    private void ShelfHasFallen()
    {
        _player.transform.parent = null;
        _player.GetComponent<PlayerStateMachine>().enabled = true;
        _player.GetComponent<PlayerStateMachine>().IsClimbing = false;
        _player.attachedRigidbody.isKinematic = false;
        transform.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other;
            _player.transform.parent = transform;
            _player.GetComponent<PlayerStateMachine>().enabled = false;
            _player.attachedRigidbody.isKinematic = true;
            GetComponent<Animator>().SetTrigger("ShelfFall");
        }
    }
}