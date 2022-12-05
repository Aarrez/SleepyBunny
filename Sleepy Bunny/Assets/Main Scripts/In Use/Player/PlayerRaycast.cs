using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    internal InputScript _theInput;

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

        _theInput = FindObjectOfType<InputScript>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
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
        if (_theInput.InteractCtx.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
            {
                if (!hit.collider.CompareTag("Move_Object")) return;
                _grabObject = hit.rigidbody;
                _tether.connectedBody = _grabObject;
            }
        }
    }

    //Fall Damage Soft
}