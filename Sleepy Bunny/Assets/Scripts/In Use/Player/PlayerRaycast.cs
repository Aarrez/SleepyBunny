using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private Rigidbody _grabObject;
    private HingeJoint _tether;

    internal Collider _climbBox;

    //Player Bool List
    internal bool _grounded;

    internal bool _climbing;

    //RayCast Lengths

    private float _range = 0.5f;

    private void Awake()
    {
        _tether = GetComponentInChildren<HingeJoint>();
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
        {
            if (hit.transform.gameObject.CompareTag("Climb"))
            {
                _climbBox = hit.transform.GetComponent<Collider>();
                _climbing = true;
            }
        }
        else _climbing = false;
    }

    public void PickUp()
    {
        if (InputScript.grabCtx().performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
            {
                if (!hit.collider.CompareTag("Move_Object")) return;
                _grabObject = hit.rigidbody;
                _tether.connectedBody = _grabObject;
            }
        }
        if (InputScript.grabCtx().canceled)
        {
        }
    }

    //Fall Damage Soft
}