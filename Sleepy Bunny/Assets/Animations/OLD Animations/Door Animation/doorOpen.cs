using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public GameObject ObjectYouWishtoMove;
    public Animator objectToMove;

    private bool holdDoorHandle = false;
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player" && InputScript.grabCtx().performed)
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