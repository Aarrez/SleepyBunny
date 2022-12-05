using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class waveManager : MonoBehaviour


{ 
    public static waveManager instance;
    public float amplitude = 1.0f;
    public float length = 1.0f;
    public float speed = 1.0f;
    public float offset = 0f;

private void Awake()
    {
        if (instance == null)

        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exsits, destroying object!");
            Destroy(this);
        }

    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float _x)
    { 
        return amplitude * MathF.Sin(_x / length + offset); 
    }

    internal float GetwaveHeight(float y)
    {
        throw new NotImplementedException();
    }
}
        