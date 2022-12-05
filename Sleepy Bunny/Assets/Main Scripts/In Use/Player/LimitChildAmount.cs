using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitChildAmount : MonoBehaviour
{
    public int maxChildAmount = 1;

    void Update()
    {
        if (transform.childCount > maxChildAmount)
        {
            for (int i = maxChildAmount; i < transform.childCount; i++)
            {
                transform.GetChild(i).SetParent(null);
            }
        }
    }
}
