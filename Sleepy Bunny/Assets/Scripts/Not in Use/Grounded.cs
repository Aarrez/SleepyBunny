using System;
using UnityEngine;

public class Grounded : PlayerRaycast
{
    public static event Action touchedGround;

    private float totalTimeInAir, time;

    private bool timeCollected = true;

    public static Func<float> airTime;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        touchedGround?.Invoke();

        _grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        _grounded = false;
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