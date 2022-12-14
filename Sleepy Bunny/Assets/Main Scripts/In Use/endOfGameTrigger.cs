using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endOfGameTrigger : MonoBehaviour
{
    private GameMaster gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }
        SceneManager.LoadScene("UI Scene");
    }
}