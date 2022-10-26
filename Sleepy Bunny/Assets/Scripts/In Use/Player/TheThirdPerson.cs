using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheThirdPerson : PlayerRaycast
{
    #region Varibales

    private Rigidbody _rb;

    #region refrence scripts

    private GameMaster _gm;

    #endregion refrence scripts

    public float Speed = 1f;

    public float JumpHeight = 10f;

    public float RotationSpeed = .1f;

    public float Punchforce = 4f;

    //Move Vector
    private Vector3 _movement = Vector3.zero;

    private Vector3 _movementDirection = Vector3.zero;

    //Falling
    public Vector3 Velocity;

    #region Climbing Stuff

    [SerializeField] private float climbSpeed = 5f;

    #endregion Climbing Stuff

    // Damage stuff
    public float DamageAmount;

    private float _amountOfHealth;
    public float Health;
    public AudioSource PainNoise;
    public AudioSource Sizzle;

    public bool ShouldRespawn = false;

    #endregion Varibales

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    //Invokes when object is enabled
    private void OnEnable()
    {
        InputScript.doMove += M_Movement;

        InputScript.doGrab += PClimb;
    }

    //Invokes when object is disable
    private void OnDisable()
    {
        InputScript.doMove -= M_Movement;
        InputScript.doGrab -= PClimb;
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
        if (!_climbing)
        {
            Debug.Log("normal movement");
            _movement = new Vector3(tempV2.x, 0f, tempV2.y);
        }
        else
        {
            Debug.Log("Climbing");
            _movement = new Vector3(tempV2.x, tempV2.y, 0f);
            float objectDistance = Vector3.Distance(transform.position, _climbBox.ClosestPointOnBounds(transform.position));
            Debug.Log(objectDistance);
            if (objectDistance > .1f)
            {
                _climbing = false;
                _rb.useGravity = true;
            }
        }

        _movementDirection = Camera.main.transform.TransformDirection(_movement);
    }

    //Uses Ridgid body to rotate the player
    private void PRotate()
    {
        if (_movement == Vector3.zero) return;

        _rb.rotation = Quaternion.RotateTowards(_rb.rotation, Quaternion.LookRotation(_movementDirection, Vector3.up), RotationSpeed);
    }

    //Move the player through MovePosition
    private void PlayerMovePosition()
    {
        if (_movement == Vector3.zero) { return; }

        _rb.MovePosition(transform.position + _movementDirection * Speed * Time.fixedDeltaTime);
    }

    //Move the player though Velocity
    private void PClimbVelocity()
    {
        if (_movement == Vector3.zero) return;

        _rb.velocity = _movement * climbSpeed * Time.fixedDeltaTime;
    }

    //Gets the direction of the camera in world space
    private void PMoveDirectionOfCamera()
    {
        Vector3 camDirection = Camera.main.transform.TransformDirection(_movement);
        //Prevents the player model form leaning down when looking down
        _movementDirection = new Vector3(camDirection.x, 0f, camDirection.z);
    }

    private void PClimb()
    {
        Climbing();
        if (_climbing)
        {
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        PRotate();
        PMoveDirectionOfCamera();
        if (!_climbing)
        {
            PlayerMovePosition();
        }
        else
        {
            PClimbVelocity();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement and rotation
        PMoveDirectionOfCamera();

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
    public void ApplyJumpUpforce()
    {
        _rb.AddForce(Vector3.up * JumpHeight, ForceMode.Force);
    }

    //Punch Force
    public void PunchForce()
    {
        _rb.AddForce(Vector3.forward * Punchforce, ForceMode.Force);
    }

    //Respaawn
    public void Respawn()
    {
        transform.position = _gm.lastCheckPointPos;
    }
}