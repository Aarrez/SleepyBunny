using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillowcatcher: MonoBehaviour
{
    public bool holdit;
    public Transform holdParent;
    public Collider thingToSoften;

    void Update()
    {
        if (holdit)
        {
            //Debug.Log("can pick up");
            Rigidbody weight = GetComponent<Rigidbody>();
            //weight.useGravity = false;
            weight.isKinematic = true;
            weight.transform.parent = holdParent;


        }
        else
        {
            Rigidbody weight = GetComponent<Rigidbody>();
            weight.useGravity = true;
            weight.transform.parent = null;
            weight.isKinematic = false;
        }
    }


    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == thingToSoften)
            holdit = true;
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == thingToSoften)
            holdit = false;
    }
}



    
        
        
   

