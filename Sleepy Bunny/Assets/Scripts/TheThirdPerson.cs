using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheThirdPerson : MonoBehaviour
{
    #region Varibales

    private GameObject playerAmature;
    public Raycasts rc;
    private Rigidbody rb;
    private PlayerInput playerInput;

    #region refrence scripts

    private animation refscript;
    private GameMaster gm;
    private OtherGrab og;

    #endregion refrence scripts

    public float speed = 1f;

    public float JumpHeight = 10f;

    public float rotationSpeed = .1f;
    public float sprintSpeed = 2f;
    private bool canClimb;

    public float punchforce = 4f;

    //Move Vector
    private Vector3 movement = Vector3.zero;

    private Vector3 movementDirection = Vector3.zero;

    private Quaternion rotateTo;

    //Falling
    public Vector3 Velocity;

    [Range(0, 10)] public float fallDistance;
    private float fallStart;
    private float fallEnd;
    private bool wasGrounded;
    private bool wasFalling;

    #region Climbing Stuff

    public float climbSpeed = 5f;

    public float sticktowall = 1f;
    public float range = 2f;
    public float distanceToGround = 1.1f;

    #endregion Climbing Stuff

    //Possition in World

    private Vector3 playerOrigin;

    // Damage stuff
    public float DamageAmount;

    [Range(0, 100)] public float AmountOfHealth;
    public float health;
    public AudioSource PainNoise;
    public AudioSource Sizzle;

    public bool respawn = false;

    #endregion Varibales

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        refscript = GetComponent<animation>();
        rc = GetComponent<Raycasts>();
        playerAmature = GameObject.FindGameObjectWithTag("Bones");
        playerInput = GetComponent<PlayerInput>();
    }

    //Invokes when object is enabled
    private void OnEnable()
    {
        InputScript.doMove += M_Movement;
    }

    //Invokes when object is disable
    private void OnDisable()
    {
        InputScript.doMove -= M_Movement;
    }

    public void Start()
    {
        Physics.gravity = new Vector3(0, -9.82F, 0);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;

        playerOrigin = transform.position;
    }

    //Gets input and sets rotation on button press
    private void M_Movement()
    {
        //Basic Movement

        //Sprint

        #region deprecated sprint code

        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 0.5f;
        }*/

        #endregion deprecated sprint code

        //Gets the input
        Vector2 tempV2 = InputScript.moveCtx().ReadValue<Vector2>();
        movement = new Vector3(tempV2.x, 0f, tempV2.y);

        movementDirection = Camera.main.transform.TransformDirection(movement);
    }

    //Uses Ridgid body to rotate the player
    private void M_PRotate()
    {
        if (movement == Vector3.zero) return;

        rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(movementDirection, Vector3.up), rotationSpeed);
    }

    //Move the player through MovePosition
    private void M_PMoveMP()
    {
        if (movement == Vector3.zero) { return; }

        rb.MovePosition(transform.position + movementDirection * speed * Time.fixedDeltaTime);
    }

    //Move the player though Velocity
    private void M_PMoveV()
    {
        if (movement == Vector3.zero) return;

        rb.velocity = movement * speed * Time.fixedDeltaTime;
    }

    //Gets the direction of the camera in world space
    private void M_PMoveDirectionOfCamera()
    {
        Vector3 camDirection = Camera.main.transform.TransformDirection(movement);
        movementDirection = new Vector3(camDirection.x, 0f, camDirection.z);
    }

    private void FixedUpdate()
    {
        rc.Grounded();
        M_PRotate();
        M_PMoveDirectionOfCamera();
        M_PMoveMP();

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

        ////force Checkpoint
        //if (Input.GetKeyDown(KeyCode.Return))
        //{ Respawn(); }

        #endregion Climbing & Push & Forece Checkpoint
    }

    //Jump Force
    public void ApplyJumpUpforce()
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

    //Taking Damage
    private void TakeDamage()
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

    private bool isFalling
    { get { return (!rc.grounded && rb.velocity.y < 0); } }
}