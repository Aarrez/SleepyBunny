
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{

    public GameObject tone;
    public bool on = false;
    private bool triggered;

    // Use this for initialization
    private void Update()
    {
        if (triggered == true)
        {
            Debug.Log("8mm Camera Turned on");

            if (Input.GetKeyDown(KeyCode.E) && !on)
            {
                tone.SetActive(true);
                on = true;
            }

            else if (Input.GetKeyDown(KeyCode.E) && on)
            {
                tone.SetActive(false);
                on = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
            triggered = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
            triggered = false;
    }

}