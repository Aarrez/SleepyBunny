using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgehook : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            col.transform.parent = transform;
            col.transform.localPosition = Vector3.zero;
            Rigidbody weight = GetComponent<Rigidbody>();
            
            weight.useGravity = false;
            //weight.isKinematic = true;
            //weight.transform.parent = holdParent;
        }
        else
        {
            col.transform.parent = null;
            col.transform.localPosition = Vector3.zero;
            Rigidbody weight = GetComponent<Rigidbody>();

            weight.useGravity = true;
            //weight.isKinematic = true;
            //weight.transform.parent = holdParent;
        }

    }

  
}
