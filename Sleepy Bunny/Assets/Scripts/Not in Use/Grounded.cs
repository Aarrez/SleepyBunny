using System;
using PlayerStM.BaseStates;
using Unity.Mathematics;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private PlayerStateMachine _player;

    [SerializeField] private bool _grounded;

    [SerializeField] private float rayDistance = 2f;

    [SerializeField, Range(0, 1)] private float _vectorAngle = 0.5f;

    [Tooltip("Only change if debuging!" + "\n" +
        "Default layermask is Ground")]
    [SerializeField] private LayerMask _rayHitLayerMask;

    private Vector3[] _halfVectors = new Vector3[5];

    private static Action<bool> _isGroundedEvent;

    public static event Action<bool> IsGroundedEvent
    {
        add => _isGroundedEvent += value;
        remove => _isGroundedEvent -= value;
    }

    private void Awake()
    {
        _player = GetComponentInParent<PlayerStateMachine>();
    }

    private void Start()
    {
        _rayHitLayerMask = LayerMask.GetMask("Ground");
        SetRaycastVectors();
    }

    private void OnEnable()
    {
        InputScript.Jump += GroundedRaycast;

        //AnimationFunctionManager.FallAnimation += GroundedRaycast;
    }

    private void OnDisable()
    {
        InputScript.Jump -= GroundedRaycast;

        //AnimationFunctionManager.FallAnimation -= GroundedRaycast;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground")) return;

        _grounded = true;
        _isGroundedEvent?.Invoke(_grounded);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground")) return;

        _grounded = false;
        _isGroundedEvent?.Invoke(_grounded);
    }

    private void GroundedRaycast()
    {
        RaycastHit hit;
        float distance = 0;
        for (int i = 0; i < _halfVectors.Length; i++)
        {
            if (Physics.Raycast(transform.position, _halfVectors[i], out hit,
                rayDistance, _rayHitLayerMask))
            {
                Debug.DrawRay(transform.position, _halfVectors[i], Color.red, rayDistance);
                distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < 1)
                {
                    _grounded = false;
                    _isGroundedEvent?.Invoke(_grounded);
                }
                break;
            }
        }
    }

    private void SetRaycastVectors()
    {
        _halfVectors[0] = Vector3.down;
        _halfVectors[1] = Vector3.Lerp(Vector3.down, Vector3.back, _vectorAngle);
        _halfVectors[2] = Vector3.Lerp(Vector3.down, Vector3.forward, _vectorAngle);
        _halfVectors[3] = Vector3.Lerp(Vector3.down, Vector3.right, _vectorAngle);
        _halfVectors[4] = Vector3.Lerp(Vector3.down, Vector3.left, _vectorAngle);
    }
}