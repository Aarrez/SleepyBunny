using System.ComponentModel;
using UnityEngine;

public class Raycasts : MonoBehaviour
{
    private Rigidbody grabObject;
    private HingeJoint tether;

    //Player Bool List
    internal bool grounded;

    //RayCast Lengths

    [Range(0, 1)] public float range;

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

    //public void Climbing()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, range))
    //    {
    //        if (hit.transform.gameObject.CompareTag("Climb"))
    //        {
    //            climb = true;
    //            Debug.Log("Scramble Up Here");
    //        }
    //        else
    //        { climb = false; }
    //    }
    //}

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