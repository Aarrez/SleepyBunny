using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    //RefrenceScripts

    private TheThirdPerson newThirdPerson;
    public bool isGrounded;

    public void Start()
    {
        //nfp = GetComponent<NewThirdPerson>();
    }

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
        else
        {
            Debug.Log("Not Grounded");
            isGrounded = false;
        }
    }
}