using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherGrab : MonoBehaviour
{
    public bool pickup;
    public Transform holdParent;
    [SerializeField] private PlayerRaycast rc;

    public void PickUp()
    {
        //Debug.Log("can pick up");
        Rigidbody weight = GetComponent<Rigidbody>();
        //weight.useGravity = false;
        weight.isKinematic = true;
        weight.transform.parent = holdParent;
    }

    public void LetGo()
    {
        Rigidbody weight = GetComponent<Rigidbody>();
        weight.useGravity = true;
        weight.transform.parent = null;
        weight.isKinematic = false;
    }
}