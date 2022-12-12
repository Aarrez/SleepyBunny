using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopDrawer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Drawer") { return; }

        other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}