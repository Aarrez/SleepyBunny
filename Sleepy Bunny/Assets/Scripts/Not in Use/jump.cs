using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpheight = 10;
    public bool grounded;
    public float groundDistance = 1;
    private Vector3 playerOrigin;
    Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        playerOrigin = transform.position;
        RaycastHit hit;
        Ray myRay = new Ray(transform.position, Vector3.down);
        grounded = Physics.Raycast(myRay,out hit, groundDistance);

        if (Input.GetKeyDown(KeyCode.J) && grounded)
        {
            rb.AddForce(Vector3.up * jumpheight, ForceMode.Impulse);
            Debug.Log("Raycast Hit");
        }

        
        
    }


}
