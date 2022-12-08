using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger("OpenDoor");
    }
}