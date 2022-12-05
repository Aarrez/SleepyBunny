using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OtherGrab : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private bool Grabed = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
        gameObject.layer = LayerMask.NameToLayer("Grabable");
        gameObject.tag = "Move_Object";
    }
}