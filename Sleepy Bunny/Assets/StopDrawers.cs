using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopDrawers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Getting triggerd");
        if (other.tag != "Drawer") { return; }

        Debug.Log("Freeze position");
        other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}