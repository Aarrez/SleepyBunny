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

    private void OpenDoor(bool openDoor)
    {
        if (openDoor)
        {
            Debug.Log("By the handle");
            objectToMove.SetBool("doorOpen", true);
        }
    }
}