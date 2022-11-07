using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStM.SubStates;

using Cinemachine;

namespace PlayerStM.BaseStates
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Refrences

        #region Script Refrences

        //Input value storage
        private Action _moveing, _jump, _grab, _pause, _crouch;

        private InputAction.CallbackContext _moveCtx, _jumpCtx, _grabCtx,
            _pauseCtx, _crouchCtx;

        [SerializeField] private AudioSource _painNoise;
        [SerializeField] private AudioSource _sizzle;

        private Rigidbody _rb;

        private GameMaster _gm;

        private ControllAction thePlayerInput;

        private ControllAction.CustomPlayerActions _cPlayer;
        private ControllAction.CustomUIActions _cUI;

        private Animator _anim;

        private Collider _collider;

        private CinemachineFreeLook _mainCamera;

        //State Variables
        private BasePlayerState _playerState;

        private StateFactory _stateFactory;

        #endregion Script Refrences

        #region Variables

        private Vector3 _movement = Vector3.zero;
        private Vector3 _movementDirection = Vector3.zero;

        [Header("Movement related variables")]
        [SerializeField] private float _movmentForce = 1f;

        [SerializeField] private float _airMovemntForce = .5f;
        [SerializeField] private float _climbSpeed = 5f;
        [SerializeField] private float _jumpHeight = 10f;
        [SerializeField] private float _rotationSpeed = .1f;

        [Header("")]
        [SerializeField, Range(0, 1.5f)] private float _rCRange = 1;

        public bool ShouldRespawn = false;
        private bool _isGrounded = false;
        private bool _isClimbing = false;
        private bool _isFalling = false;
        private bool _isCrouching = false;

        #endregion Variables

        #endregion Refrences

        #region Get and set

        public Rigidbody Rb { get => _rb; set => _rb = value; }

        public Animator Anim { get => _anim; set => _anim = value; }

        public event Action Moveing
        {
            add => _moveing += value;
            remove => _moveing -= value;
        }

        public event Action Jump
        {
            add => _jump += value;
            remove => _jump -= value;
        }

        public event Action Grab
        {
            add => _grab += value;
            remove => _grab -= value;
        }

        public event Action Pause
        {
            add => _grab += value;
            remove => _grab -= value;
        }

        public event Action Crouch
        {
            add => _crouch += value;
            remove => _crouch -= value;
        }

        public BasePlayerState PlayerState
        { get { return _playerState; } set { _playerState = value; } }

        public float MovmentForce
        { get => _movmentForce; set => _movmentForce = value; }

        public float JumpHeight
        { get => _jumpHeight; set => _jumpHeight = value; }

        public float RotationSpeed
        { get => _rotationSpeed; set => _rotationSpeed = value; }

        public float AirMovemntForce
        { get => _airMovemntForce; set => _airMovemntForce = value; }

        public bool IsGrounded => _isGrounded;
        public bool IsClimbing => _isClimbing;
        public bool IsFalling => _isFalling;
        public bool IsCrouching => _isCrouching;

        public InputAction.CallbackContext MoveCtx => _moveCtx;
        public InputAction.CallbackContext JumpCtx => _jumpCtx;
        public InputAction.CallbackContext GrabCtx => _grabCtx;
        public InputAction.CallbackContext PauseCtx => _pauseCtx;
        public InputAction.CallbackContext CrouchCtx => _crouchCtx;

        #endregion Get and set

        private void Awake()
        {
            thePlayerInput = new ControllAction();
            _cPlayer = thePlayerInput.CustomPlayer;
            _cUI = thePlayerInput.CustomUI;

            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();

            //set state
            _stateFactory = new StateFactory(this);
            _playerState = _stateFactory.SuperGrounded();
            _playerState.EnterState();
        }

        private void Start()
        {
            Physics.gravity = new Vector3(0, -9.82F, 0);
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            _mainCamera = camera.GetComponent<CinemachineFreeLook>();
            _mainCamera.Follow = transform;
            _mainCamera.LookAt = transform;
        }

        private void OnEnable()
        {
            // Gets all the inputs from the InputActionMap and Invokes events
            // when a button/stick is interacted with

            #region Input Stuff

            #region Movement Input

            _cPlayer.Movement.performed += ctx =>
            {
                _moveing?.Invoke();
                _moveCtx = ctx;
            };

            _cPlayer.Movement.canceled += ctx =>
            {
                _moveing?.Invoke();
                _moveCtx = ctx;
            };

            #endregion Movement Input

            #region Jump Input

            _cPlayer.Jump.performed += ctx =>
            {
                _jump?.Invoke();
                _jumpCtx = ctx;
            };

            #endregion Jump Input

            #region Grab Input

            _cPlayer.Grab.performed += ctx =>
            {
                _grab?.Invoke();
                _grabCtx = ctx;
            };

            _cPlayer.Grab.canceled += ctx =>
            {
                _grab?.Invoke();
                _grabCtx = ctx;
            };

            #endregion Grab Input

            #region Pause Input

            _cUI.Pause.performed += ctx =>
            {
                _pause?.Invoke();
                _pauseCtx = ctx;
            };

            #endregion Pause Input

            #region Crouch Input

            _cPlayer.Crouch.performed += ctx =>
            {
                _crouch?.Invoke();
                _crouchCtx = ctx;
            };

            _cPlayer.Crouch.canceled += ctx =>
            {
                _crouch?.Invoke();
                _crouchCtx = ctx;
            };

            #endregion Crouch Input

            _cPlayer.Enable();
            _cUI.Enable();

            #endregion Input Stuff

            Grounded.IsGroundedEvent += CheckGrounded;
        }

        private void OnDisable()
        {
            _cPlayer.Disable();
            _cUI.Disable();

            Grounded.IsGroundedEvent -= CheckGrounded;
        }

        private void FixedUpdate()
        {
            _playerState.UpdateStates();
        }

        private void CheckGrounded(bool grounded)
        {
            _isGrounded = grounded;
        }

        public void Climbing()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rCRange))
            {
                if (hit.transform.gameObject.CompareTag("Climb"))
                {
                    _collider = hit.transform.GetComponent<Collider>();
                    _isClimbing = true;
                }
            }
        }
    }
}