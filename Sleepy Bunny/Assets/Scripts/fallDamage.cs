using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fallDamage : MonoBehaviour

{

    public Rigidbody RidgedBody;
    public bool IsAlive = true;
    public double _decelerationTolerance = -2.5;
    public Vector3 SpawnPoint;

    Raycasts rc;


    void Start()
    {
        
    }
    void FixedUpdate()
    {

        if (RidgedBody.velocity.y < _decelerationTolerance)
        {

            transform.position = SpawnPoint;
            IsAlive = false;

        }

    }
    
}
