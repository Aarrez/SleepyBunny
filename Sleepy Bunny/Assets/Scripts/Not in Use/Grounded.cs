using System;
using UnityEngine;

public class Grounded : Raycasts
{
    public static event Action touchedGround;

    public static Func<float> airTime;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        touchedGround?.Invoke();
        grounded = true;
        Debug.Log("touch grass");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground")) return;
        Debug.Log("no touch grass");
        grounded = false;
    }
}