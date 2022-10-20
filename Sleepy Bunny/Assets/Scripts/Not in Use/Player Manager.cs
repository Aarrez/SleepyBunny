using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    public static bool isGameOver;

   

    void TakeDamage(float damageAmount)
    {
        
        if(playerHP <=0)
        {
            isGameOver = true;
        }
    }
}
