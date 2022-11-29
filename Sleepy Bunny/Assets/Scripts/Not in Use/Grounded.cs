using System;
using PlayerStM.BaseStates;
using Unity.Mathematics;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private PlayerStateMachine _player;

    [SerializeField] private bool _grounded;

    private static Action _isGroundedEvent;

    public static event Action IsGroundedEvent
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;

        _isGroundedEvent?.Invoke();
    }
}