using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;
    public PlayerAnimatonManager am;

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimatonManager>();
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