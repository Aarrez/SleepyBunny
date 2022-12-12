using System;
using PlayerStM.BaseStates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OtherGrab : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public bool Grabed = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        gameObject.layer = LayerMask.NameToLayer("Grabable");
        if (gameObject.CompareTag("Untagged"))
        {
            gameObject.tag = "Move_Object";
        }
    }

    public void ObjectGrabed(bool grabed)
    {
        Grabed = grabed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) { return; }
        collision.collider.GetComponentInChildren<Animator>().SetFloat("MoveIndex", 3);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) { return; }

        collision.collider.GetComponentInChildren<Animator>().SetFloat("MoveIndex", 0);
    }
}