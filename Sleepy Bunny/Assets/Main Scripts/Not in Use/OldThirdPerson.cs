/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldThirdPerson : InputScript

{
    //refrence scripts
    public Raycasts rc;

    private PlayerAnimatonManager refscript;
    private GameMaster gm;
    private OtherGrab og;
    private InputScript scriptInput;

    public float speed = 1f;

    public float JumpHeight = 10f;

    private Rigidbody rb;
    public float rotationSpeed = .1f;
    public float sprintSpeed = 2f;
    private bool canClimb;

    public float punchforce = 4f;

    //Move Vector
    private Vector3 Movement;

    //Falling
    public Vector3 Velocity;

    [Range(0, 10)] public float fallDistance;
    private float fallStart;
    private float fallEnd;
    private bool wasGrounded;
    private bool wasFalling;

    //Climbing Stuff
    public float climbSpeed = 5f;

    public float sticktowall = 1f;
    public float range = 2f;
    public float distanceToGround = 1.1f;

    //Possition in World

    private Vector3 playerOrigin;

    // Damage stuff
    public float DamageAmount;

    [Range(0, 100)] public float AmountOfHealth;
    public float health;
    public AudioSource PainNoise;
    public AudioSource Sizzle;

    public bool respawn = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        refscript = GetComponent<PlayerAnimatonManager>();
        rc = GetComponent<Raycasts>();

        doMove += M_Movement;
    }

    public void Start()
    {
        Physics.gravity = new Vector3(0, -10F, 0);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    private void FixedUpdate()
    {
        if (!wasFalling && isFalling)
            fallStart = transform.position.y;
        if (!wasGrounded && rc.grounded)
            TakeDamage();

        wasGrounded = rc.grounded;
        wasFalling = isFalling;
    }

    // Update is called once per frame
    void Update()
    {
        //Basic Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 targetDirection = new Vector3(h, 0f, v);
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;
        Vector3 movementDirection = targetDirection;
        movementDirection.Normalize();

        //Sprint
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = speed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 0.5f;
            }
        }

        //Jump Jumpm is triggered in animation!

        //if (rc.grounded && Input.GetKeyDown(KeyCode.Space))
        //{
        //    ApplyJumpUpforce();
        //}

        //Looking Direction
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;

            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            rb.MovePosition(movementDirection * speed * Time.deltaTime);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }

        //Pulling
        //if(refscript.isPulling == true)
        //{
        //    transform.forward = -transform.forward;
        //}
        //else
        //{
        //    refscript.isPulling = false;
        //    transform.forward = transform.forward;
        //}

        //Climbing
        if (rc.climb && Input.GetKey(KeyCode.E))

        {
            transform.position += transform.up * Time.deltaTime * climbSpeed;
            rb.useGravity = false;
        }
        else
        { rb.useGravity = true; }

        //Punch
        if (rc.pushOrPull && Input.GetKeyDown(KeyCode.P))
        {
            PunchForce();
        }

        //force Checkpoint
        if (Input.GetKeyDown(KeyCode.Return))
        { Respawn(); }

        FallDamage();
    }

    //MoveCtx
    private void M_Movement()
    {
        Movement = moveCtx().ReadValue<Vector3>();
    }

    //Jump Force
    public void ApplyJumpUpforce()
    {
        rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
    }

    //Punch Force
    public void PunchForce()
    {
        rb.AddForce(Vector3.forward * punchforce, ForceMode.Impulse);
    }

    //Respaawn
    public void Respawn()
    {
        transform.position = gm.lastCheckPointPos;
    }

    //Taking Damage
    void TakeDamage()
    {
        float fallLength = fallStart - transform.position.y;

        if (!rc.soft && fallLength > fallDistance)

        {
            health = AmountOfHealth - DamageAmount;
            PainNoise.Play();
            Respawn();
        }

        if (rc.grounded && rc.canKill)
        {
            PainNoise.Play();
            Respawn();
            Sizzle.Play();
        }
    }

    //Fall Damage

    void FallDamage()
    {
    }

    private bool isFalling
    { get { return (!rc.grounded && rb.velocity.y < 0); } }
}
*/