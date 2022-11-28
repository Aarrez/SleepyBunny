using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour
{
    public GameObject LightSwitch;
    public bool on = false;
    private bool triggered;
    public Material materialOn;
    public GameObject objectToChange;
    public Renderer objectToRender;

    // Use this for initialization

    public void Start()
    {
        //GetComponent<GameObject>
    }

    public void Update()
    {
        if (triggered == true)
        {
            Debug.Log("Light Is On");

            if (Input.GetKeyDown(KeyCode.E) && !on)
            {
                LightSwitch.SetActive(true);
                on = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && on)
            {
                LightSwitch.SetActive(false);
                on = false;
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
            triggered = true;
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
            triggered = false;
    }
}