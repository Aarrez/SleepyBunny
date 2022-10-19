using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour

{
    private TheThirdPerson nfp;
    [SerializeField] private Raycasts rc;

    //public groundedcollider isGroundedScript;
    public bool isGrounded;

    public bool pickMeUp;
    public bool isPulling;

    private bool wasGrounded;
    private bool wasFalling;
    private float startOfFall;

    private Vector3 enterHeight;
    private Vector3 exitHeight;

    public float fallHeight;

    public Rigidbody RidgedBody;
    public bool isAlive = true;
    public double _decelerationTolerance = 0;

    //public Vector3 SpawnPoint;
    public bool canKillOnTouch;

    private GameMaster gm;
    private Vector3 playerOrigin;
    public Animator anim;

    private bool isFalling
    {
        get
        { return (!rc.grounded && RidgedBody.velocity.y < 0); }
    }

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
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
        if (!rc.grounded) return;

        anim.SetBool("walk", false);
        anim.SetBool("jump", true);
        anim.SetBool("idle", false);
        anim.SetBool("push", false);

        if (rc.pushOrPull)
            isPulling = false;
    }

    private void M_AnimGrab()
    {
        if (!rc.pushOrPull) return;
        anim.SetBool("walk", false);
        anim.SetBool("jump", false);
        anim.SetBool("idle", false);
        anim.SetBool("push", true);
        isPulling = true;
    }

    public void Update()
    {
        //if (rc.pushOrPull && Input.GetKey(KeyCode.Space))
        //{
        //    anim.SetBool("walk", false);
        //    anim.SetBool("jump", true);
        //    anim.SetBool("idle", false);
        //    anim.SetBool("push", false);
        //}
    }

    //IEnumerator Respawn()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    transform.position = SpawnPoint;
    //    IsAlive = false;
    //    yield return new WaitForSeconds(1);
    //    Debug.Log("Respawn");
    //}
}