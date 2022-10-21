using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatonManager : Raycasts
{
    [SerializeField] private bool startAtCheckpoint;
    private GameMaster gm;
    public Animator anim;
    public Rigidbody RidgedBody;

    //public groundedcollider isGroundedScript;
    public bool isGrounded;

    public bool pickMeUp;
    public bool isPulling;

    public float fallHeight;

    public bool isAlive = true;

    public double _decelerationTolerance = 0;

    //public Vector3 SpawnPoint;
    public bool canKillOnTouch;

    private void Start()
    {
        M_SpawnAtCheckpoint(startAtCheckpoint);
    }

    //private bool isFalling
    //{
    //    get
    //    { return (!grounded && RidgedBody.velocity.y < 0); }
    //}

    private void OnEnable()
    {
        #region Subscribeing Methods to delegets

        InputScript.doMove += M_AnimMoveIdle;
        InputScript.doJump += M_AnimJump;
        InputScript.doGrab += M_AnimGrab;

        Grounded.touchedGround += M_JumpLanded;

        #endregion Subscribeing Methods to delegets
    }

    private void OnDisable()
    {
        #region Unsubscribing Methods from delegets

        InputScript.doMove -= M_AnimMoveIdle;
        InputScript.doJump -= M_AnimJump;
        InputScript.doGrab -= M_AnimGrab;

        Grounded.touchedGround -= M_JumpLanded;

        #endregion Unsubscribing Methods from delegets
    }

    private void M_SpawnAtCheckpoint(bool start)
    {
        if (start == false) return;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    private void M_AnimMoveIdle()
    {
        if (grounded) { return; }
        if (InputScript.moveCtx().ReadValue<Vector2>() == Vector2.zero)
        {
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);
        }
    }

    private void M_AnimJump()
    {
        if (!grounded)
        {
            anim.SetBool("walk", false);
            anim.SetBool("idle", false);
            anim.SetTrigger("jump");
        }
    }

    private void M_JumpLanded()
    {
        anim.SetBool("walk", false);
        anim.SetBool("idle", true);
    }

    private void M_AnimGrab()
    {
        if (grounded) return;

        Debug.Log("Doing");
        if (InputScript.grabCtx().performed)
        {
            anim.SetBool("push", true);
        }

        if (InputScript.grabCtx().canceled)
        {
            anim.SetBool("push", false);
        }
    }
}