using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : Raycasts
{
    private TheThirdPerson nfp;

    //public groundedcollider isGroundedScript;
    public bool isGrounded;

    public bool pickMeUp;
    public bool isPulling;

    public float fallHeight;

    public Rigidbody RidgedBody;
    public bool isAlive = true;
    [SerializeField] private bool startAtCheckpoint;
    public double _decelerationTolerance = 0;

    //public Vector3 SpawnPoint;
    public bool canKillOnTouch;

    private GameMaster gm;
    public Animator anim;

    public override void Start()
    {
        base.Start();

        M_SpawnAtCheckpoint(startAtCheckpoint);
    }

    private bool isFalling
    {
        get
        { return (!grounded && RidgedBody.velocity.y < 0); }
    }

    private void OnEnable()
    {
        InputScript.doMove += M_AnimMoveIdle;
        InputScript.doJump += M_AnimJump;
        InputScript.doGrab += M_AnimGrab;
    }

    private void OnDisable()
    {
        InputScript.doMove -= M_AnimMoveIdle;
        InputScript.doJump -= M_AnimJump;
        InputScript.doGrab -= M_AnimGrab;
    }

    private void M_SpawnAtCheckpoint(bool start)
    {
        if (start == false) return;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    private void M_AnimMoveIdle()
    {
        if (InputScript.moveCtx().ReadValue<Vector2>() == Vector2.zero)
        {
            anim.SetBool("jump", false);
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
            anim.SetBool("push", false);
        }
        else
        {
            anim.SetBool("walk", true);
            anim.SetBool("jump", false);
            anim.SetBool("idle", false);
            anim.SetBool("push", false);
        }
    }

    private void M_AnimJump()
    {
        if (!grounded && InputScript.jumpCtx().performed)
        {
            anim.SetBool("walk", false);
            anim.SetBool("jump", true);
            anim.SetBool("idle", false);
            anim.SetBool("push", false);
        }
        else if (InputScript.jumpCtx().canceled)
        {
            anim.SetBool("walk", false);
            anim.SetBool("jump", false);
            anim.SetBool("idle", true);
            anim.SetBool("push", false);
        }

        if (pushOrPull)
            isPulling = false;
    }

    private void M_AnimGrab()
    {
        if (!pushOrPull) return;
        anim.SetBool("walk", false);
        anim.SetBool("jump", false);
        anim.SetBool("idle", false);
        anim.SetBool("push", true);
        isPulling = true;
    }
}