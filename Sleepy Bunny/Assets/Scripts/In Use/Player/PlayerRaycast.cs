using System.ComponentModel;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private Rigidbody grabObject;
    private HingeJoint tether;

    //Player Bool List
    internal bool grounded;

    internal bool climbing;

    //RayCast Lengths

    private float range = 0.5f;

    private void Awake()
    {
        tether = GetComponentInChildren<HingeJoint>();
    }

    private void OnEnable()
    {
        //InputScript.doGrab += PickUp;
    }

    private void OnDisable()
    {
        //InputScript.doGrab -= PickUp;
    }

    public void Climbing()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.CompareTag("Climb"))
            {
                climbing = true;
            }
        }
        else climbing = false;
    }

    public void PickUp()
    {
        if (InputScript.grabCtx().performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (!hit.collider.CompareTag("Move_Object")) return;
                grabObject = hit.rigidbody;
                tether.connectedBody = grabObject;
            }
        }
        if (InputScript.grabCtx().canceled)
        {
        }
    }

    //Fall Damage Soft
}