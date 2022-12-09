using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    private enum _addForce
    {
        Up = 0,

        Down = 1
    }

    [SerializeField] private _addForce AddForce;

    [SerializeField] private ForceMode _forceType = ForceMode.Force;

    [SerializeField] private float _forceModifier = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") { return; }

        if (AddForce == _addForce.Up)
        {
            other.attachedRigidbody.AddForce(Vector3.up * _forceModifier, _forceType);
        }
        else
        {
            other.attachedRigidbody.AddForce(Vector3.down * _forceModifier, _forceType);
        }
    }
}