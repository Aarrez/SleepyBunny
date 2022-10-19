using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endOfGameTrigger : MonoBehaviour
{
    GameMaster gm;
    

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("UI Scene");
    }
}
