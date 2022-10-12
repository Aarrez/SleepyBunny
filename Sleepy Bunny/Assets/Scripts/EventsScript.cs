using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsScript : MonoBehaviour
{
    public GameMaster gm;
    public GameObject object2; 
    public bool Event = false; 
    
    public void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.gameObject == object2)
        {
            Event = true;
            Debug.Log(object2 + "TARGET ACCOMPLISHED");
        }



    }
}
