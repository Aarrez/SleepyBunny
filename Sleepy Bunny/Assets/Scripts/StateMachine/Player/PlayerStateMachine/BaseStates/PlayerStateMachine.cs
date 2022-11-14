using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStM.SubStates;

using Cinemachine;
using JetBrains.Annotations;

namespace PlayerStM.BaseStates
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Refrences

        #region Script Refrences

        //Input value storage
        private Action _moveing, _jump, _grab, _pause, _crouch, _climb;

        private InputAction.CallbackContext _moveCtx, _jumpCtx, _grabCtx,
            _pauseCtx, _crouchCtx;

        [SerializeField] private AudioSource _painNoise;
        [SerializeField] private AudioSource _sizzle;

        private Rigidbody _rb;

        private GameMaster _gm;

        private ControllAction thePlayerInput;

        private ControllAction.CustomPlayerActions _cPlayer;
        private ControllAction.CustomUIActions _cUI;

        private Collider _collider;

        private Camera _mainCamera;

        //State Variables
        private BasePlayerState _currentSuper;

        private BasePlayerState _currentSub;

        private StateFactory _stateFactory;

        #endregion Script Refrences

        #region Variables

        [Header("Movement related variables")]
        [Tooltip("Determines how speedy the character is")]
        [SerializeField] private float _movmentForce = 1f;

        [Tooltip("Determines how much directional force is applyed when jumping")]
        [SerializeField] private float _directionalJumpForce = .5f;

        [Tooltip("Determines how high you jump")]
        [SerializeField] private float _jumpHeight = 10f;

        [SerializeField] private float _climbSpeed = 5f;
        [SerializeField] private float _rotationSpeed;

        [Header("")]
        [SerializeField, Range(0, 1.5f)] private float _rCRange = 1;

        private Animator _playerAnimator;

        public bool ShouldRespawn = false;
        private bool _isGrounded = false;
        private bool _isClimbing = false;
        private bool _isFalling = false;
        private bool _isCrouching = false;
        private bool _isGrabing = false;

        #endregion Variables

        #endregion Refrences

        #region Get and set

        public Rigidbody Rb { get => _rb; set => _rb = value; }

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

        public BasePlayerState CurrentSuper
        {
            get { return _currentSuper; }
            set { _currentSuper = value; }
        }

        public BasePlayerState CurrentSub
        {
            get { return _currentSub; }
            set { _currentSub = value; }
        }

        public float MovmentForce
        { get => _movmentForce; set => _movmentForce = value; }

        public float JumpHeight
        { get => _jumpHeight; set => _jumpHeight = value; }

        public float RotationSpeed => _rotationSpeed;

        public float DirectionalJumpForce
        { get => _directionalJumpForce; set => _directionalJumpForce = value; }

        public bool IsGrounded => _isGrounded;
        public bool IsClimbing => _isClimbing;
        public bool IsFalling => _isFalling;
        public bool IsCrouching => _isCrouching;
        public bool IsGrabing => _isGrabing;

        public InputAction.CallbackContext MoveCtx => _moveCtx;
        public InputAction.CallbackContext JumpCtx => _jumpCtx;
        public InputAction.CallbackContext GrabCtx => _grabCtx;
        public InputAction.CallbackContext PauseCtx => _pauseCtx;
        public InputAction.CallbackContext CrouchCtx => _crouchCtx;

        /// <summary>
        /// When setting GSIndex(Integer)
        /// <br></br>
        /// Idle = 0,  Walking = 1,
        /// <br></br>
        /// Falling = 2, Run = 3(Might not be used)
        /// </summary>
        public Animator PlayerAnimator => _playerAnimator;

        public Camera MainCamera => _mainCamera;

        #endregion Get and set

        private void Awake()
        {
            thePlayerInput = new ControllAction();
            _cPlayer = thePlayerInput.CustomPlayer;
            _cUI = thePlayerInput.CustomUI;

            _playerAnimator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody>();

            _mainCamera = Camera.main;
        }

        private void Start()
        {
            Physics.gravity = new Vector3(0, -9.82F, 0);
            //set state
            _stateFactory = new StateFactory(this);
            _currentSuper = _stateFactory.SuperGrounded();
            _currentSuper.EnterState();
        }

        private void OnEnable()
        {
            _playerAnimator.SetBool("Enabled", true);
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

            _cPlayer.DebugState.performed += ctx => GetCurrentState();

            _cPlayer.Enable();
            _cUI.Enable();

            #endregion Input Stuff

            Grounded.IsGroundedEvent += CheckGrounded;
        }

        private void OnDisable()
        {
            _playerAnimator.SetBool("Enabled", false);
            _cPlayer.Disable();
            _cUI.Disable();

            Grounded.IsGroundedEvent -= CheckGrounded;
        }

        private void FixedUpdate()
        {
            _currentSuper.UpdateStates();
        }

        private void CheckGrounded(bool grounded)
        {
            _isGrounded = grounded;
        }

        private void GetCurrentState()
        {
            Debug.Log("SuperState " + CurrentSuper);
            Debug.Log("SubState: " + CurrentSub);
        }

        public void ClimbGrab(RaycastHit hit)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, _rCRange))
            {
                if (hit.transform.gameObject.CompareTag("Climb"))
                {
                    _isClimbing = true;
                    Debug.Log("Hit climbable object");
                }
                else if (hit.transform.gameObject.CompareTag("Move_Object"))
                {
                    Debug.Log("Hit movable object");
                    _isGrabing = true;
                }
            }
        }
    }
}