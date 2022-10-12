using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject tourchLight;
    bool tourchOn;


    // Start is called before the first frame update
    void Start()
    {
        tourchLight.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tourchLight.SetActive(true);
        }

        else if (tourchLight && Input.GetKeyDown(KeyCode.Alpha1))

        {
            tourchLight.SetActive(false);
        }
       
    }
}
