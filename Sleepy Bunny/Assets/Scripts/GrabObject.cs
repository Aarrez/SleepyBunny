using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public float pickUpRange = 5;
    private GameObject grabber;
    public Transform holdParent;
    public float moveForce = 250;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       if (Input.GetKey(KeyCode.E))
        {
            if (grabber == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }

        }

       if (grabber !=null)
        {
            MoveObject();
        }

    }
    void MoveObject()
    {
        if (Vector3.Distance(grabber.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - grabber.transform.position);
            grabber.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject (GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            //objRig.useGravity = false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            grabber = pickObj;
        }
    }

    void DropObject ()
    {
        Rigidbody heldRig = grabber.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        grabber.transform.parent = null;
        grabber = null;


    }
}
