using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheThirdPerson : MonoBehaviour
{
    #region Varibales

    private GameObject GrabObject;
    private GameObject playerAmature;
    private Rigidbody rb;

    [SerializeField] private AnimationCurve animCurve;

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

    private bool climbing = false;

    public float climbSpeed = 5f;

    public float sticktowall = 1f;
    public float range = 2f;
    public float distanceToGround = 1.1f;

    #endregion Climbing Stuff

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
        playerAmature = GameObject.FindGameObjectWithTag("Bones");
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
            movement = new Vector3(tempV2.x, 0f, tempV2.y);
        }
        else
        {
            movement = new Vector3(0f, tempV2.x, tempV2.y);
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
        M_PRotate();
        M_PMoveDirectionOfCamera();
        M_PMoveMP();
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