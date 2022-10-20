using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;
    public animation am;

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<animation>();
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            Debug.Log("CheckPoint!");
        }
    }
}