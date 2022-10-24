using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheThirdPerson : PlayerRaycast
{
    #region Varibales

    private Rigidbody rb;

    #region refrence scripts

    private GameMaster gm;

    #endregion refrence scripts

    public float speed = 1f;

    public float JumpHeight = 10f;

    public float rotationSpeed = .1f;

    public float punchforce = 4f;

    //Move Vector
    private Vector3 movement = Vector3.zero;

    private Vector3 movementDirection = Vector3.zero;

    //Falling
    public Vector3 Velocity;

    #region Climbing Stuff

    [SerializeField] private float climbSpeed = 5f;

    #endregion Climbing Stuff

    // Damage stuff
    public float DamageAmount;

    private float AmountOfHealth;
    public float health;
    public AudioSource PainNoise;
    public AudioSource Sizzle;

    public bool respawn = false;

    #endregion Varibales

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Invokes when object is enabled
    private void OnEnable()
    {
        InputScript.doMove += M_Movement;
        InputScript.doGrab += M_PClimb;
    }

    //Invokes when object is disable
    private void OnDisable()
    {
        InputScript.doMove -= M_Movement;
        InputScript.doGrab -= M_PClimb;
    }

    public void Start()
    {
        Physics.gravity = new Vector3(0, -9.82F, 0);
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //transform.position = gm.lastCheckPointPos;
    }

    //Gets input and sets rotation on button press
    private void M_Movement()
    {
        //Gets the input
        Vector2 tempV2 = InputScript.moveCtx().ReadValue<Vector2>();
        if (!climbing)
        {
            Debug.Log("normal movement");
            movement = new Vector3(tempV2.x, 0f, tempV2.y);
        }
        else
        {
            Debug.Log("Climbing");
            movement = new Vector3(tempV2.x, tempV2.y, 0f);
        }

        movementDirection = Camera.main.transform.TransformDirection(movement);
    }

    //Uses Ridgid body to rotate the player
    private void M_PRotate()
    {
        if (movement == Vector3.zero) return;

        rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(movementDirection, Vector3.up), rotationSpeed);
    }

    //Move the player through MovePosition
    private void M_PlayerMovePosition()
    {
        if (movement == Vector3.zero) { return; }

        rb.MovePosition(transform.position + movementDirection * speed * Time.fixedDeltaTime);
    }

    //Move the player though Velocity
    private void M_PClimbVelocity()
    {
        if (movement == Vector3.zero) return;

        rb.velocity = movement * climbSpeed * Time.fixedDeltaTime;
    }

    //Gets the direction of the camera in world space
    private void M_PMoveDirectionOfCamera()
    {
        Vector3 camDirection = Camera.main.transform.TransformDirection(movement);
        //Prevents the player model form leaning down when looking down
        movementDirection = new Vector3(camDirection.x, 0f, camDirection.z);
    }

    private void M_PClimb()
    {
        Climbing();
        if (climbing)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        M_PRotate();
        M_PMoveDirectionOfCamera();
        if (!climbing)
        {
            M_PlayerMovePosition();
        }
        else
        {
            M_PClimbVelocity();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement and rotation
        M_PMoveDirectionOfCamera();

        #region Climbing & Push & Forece Checkpoint

        //Climbing
        //if (rc.climb && Input.GetKey(KeyCode.E))
        //{
        //    transform.position += transform.up * Time.deltaTime * climbSpeed;
        //    rb.useGravity = false;
        //}
        //else
        //{ rb.useGravity = true; }

        ////Punch
        //if (rc.pushOrPull && Input.GetKeyDown(KeyCode.P))
        //{
        //    PunchForce();
        //}

        //force Checkpoint
        //if (Input.GetKeyDown(KeyCode.Return))
        //{ Respawn(); }

        #endregion Climbing & Push & Forece Checkpoint
    }

    //Jump Force
    public void M_ApplyJumpUpforce()
    {
        rb.AddForce(Vector3.up * JumpHeight, ForceMode.Force);
    }

    //Punch Force
    public void PunchForce()
    {
        rb.AddForce(Vector3.forward * punchforce, ForceMode.Force);
    }

    //Respaawn
    public void Respawn()
    {
        transform.position = gm.lastCheckPointPos;
    }
}