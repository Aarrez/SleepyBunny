using System;
using PlayerStM.BaseStates;
using Unity.Mathematics;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private PlayerStateMachine _player;

    [SerializeField] private bool _grounded;

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
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Ground")) return;

    //    _grounded = true;
    //    _isGroundedEvent?.Invoke(_grounded);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (!other.CompareTag("Ground")) return;

    //    _grounded = false;
    //    _isGroundedEvent?.Invoke(_grounded);
    //}
}