using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public GameObject ObjectYouWishtoMove;

    public Animator objectToMove;

    private bool holdDoorHandle = false;

    public GameObject player;

    private InputScript _theInput;

    public void Start()
    {
        _theInput = FindObjectOfType<InputScript>();
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player" && _theInput.InteractCtx.performed)
        {
            holdDoorHandle = true;
            Debug.Log("By the handle");
            objectToMove.SetBool("doorOpen", true);
        }
        else if (collision.tag != "Player")
        {
            holdDoorHandle = false;
        }
    }
}