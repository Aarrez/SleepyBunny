using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OtherGrab : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public bool Grabed = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}