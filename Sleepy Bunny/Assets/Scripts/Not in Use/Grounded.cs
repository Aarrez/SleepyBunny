using System;

using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] private bool _grounded;

    private static Action<bool> _isGroundedEvent;

    private float totalTimeInAir, time;

    private bool timeCollected = true;

    public static Func<float> airTime;

    public static event Action<bool> IsGroundedEvent
    { add => _isGroundedEvent += value; remove => _isGroundedEvent -= value; }

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

    private void FixedUpdate()
    {
        if (!_grounded)
        {
            time += Time.fixedDeltaTime;
            timeCollected = false;
        }
        else if (!timeCollected && _grounded)
        {
            totalTimeInAir = time;
            time = 0f;
            timeCollected = true;
        }
    }
}