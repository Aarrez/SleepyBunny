using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStM.SubStates;

namespace PlayerStM.BaseStates
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Script Refrences

        private InputScript _theInput;

        //Input value storage

        private Action _landAnimationDoneEvent;

        //[SerializeField] private AudioSource _painNoise;

        //[SerializeField] private AudioSource _sizzle;

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

        private BasePlayerState _currentMinor;

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

        [Tooltip("Changes how fast the character turns around")]
        [SerializeField] private float _rotationSpeed;

        [Header("Raycast to ground variables")]
        [Tooltip("How long the ray is that checks for ground" +
            "\n" + "(Should be 0.1 unless debuging)")]
        [SerializeField] private float _rayGroundDist = 0.1f;

        [Tooltip("Changes the angle in which the cone of rays are shot" +
            "\n" + "0 = 0 degres(Strait down), 1 = 90 degres")]
        [SerializeField, Range(0, 1)] private float _vectorAngle = 0.5f;

        [Tooltip("Only change if debuging!" + "\n" +
            "Default layermask is Ground")]
        [SerializeField] private LayerMask _rayHitLayerMask;

        private Vector3[] _halfVectors = new Vector3[9];

        [Header("Raycast to climb")]
        [SerializeField, Range(0, 1.5f)] private float _rCRange = 1;

        private Animator _playerAnimator;

        public bool ShouldRespawn = false;

        private bool _isGrounded = false;

        private bool _isClimbing = false;

        private bool _isFalling = false;

        private bool _isCrouching = false;

        private bool _isGrabing = false;

        private bool _landAnimationDone = false;

        #endregion Variables

        #region Get and set

        public InputScript TheInput => _theInput;

        public Rigidbody Rb { get => _rb; set => _rb = value; }

        public Action LandAnimationDoneEvent
        {
            get => _landAnimationDoneEvent;
            set => _landAnimationDoneEvent = value;
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

        public BasePlayerState CurrentMinor => _currentMinor;

        public float MovmentForce
        { get => _movmentForce; set => _movmentForce = value; }

        public float JumpHeight
        { get => _jumpHeight; set => _jumpHeight = value; }

        public float RotationSpeed => _rotationSpeed;

        public float DirectionalJumpForce
        { get => _directionalJumpForce; set => _directionalJumpForce = value; }

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public bool IsClimbing => _isClimbing;

        public bool IsFalling => _isFalling;

        public bool IsCrouching => _isCrouching;

        public bool IsGrabing => _isGrabing;

        public bool LandAnimationDone
        {
            get => _landAnimationDone;
            set => _landAnimationDone = value;
        }

        /// <summary>
        /// When setting GSIndex(Integer)
        /// <br></br>
        /// Idle = 0,  Walking = 1,
        /// <br></br>
        /// Falling = 2, Land(blendTree) = 3
        /// <br></br>
        /// LandEffect: SoftLand = 1;
        /// HardLanding = 2, dead = 3(not yet implemented)
        /// </summary>
        public Animator PlayerAnimator => _playerAnimator;

        public Camera MainCamera => _mainCamera;

        #endregion Get and set

        private void Awake()
        {
            _theInput = FindObjectOfType<InputScript>();

            thePlayerInput = new ControllAction();

            _cPlayer = thePlayerInput.CustomPlayer;

            _playerAnimator = GetComponentInChildren<Animator>();
            _rb = GetComponentInParent<Rigidbody>();

            _stateFactory = new StateFactory(this);
            _currentSuper = _stateFactory.SuperGrounded();
            _currentSuper.EnterState();

            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _rayHitLayerMask = LayerMask.GetMask("Ground");
            SetRaycastVectors();

            Physics.gravity = new Vector3(0, -9.82F, 0);
        }

        private void OnEnable()
        {
            thePlayerInput.CustomPlayer.DebugState.performed += ctx => GetCurrentState();

            thePlayerInput.Enable();

            Grounded.IsGroundedEvent += CheckGrounded;
        }

        private void OnDisable()
        {
            thePlayerInput.Enable();

            Grounded.IsGroundedEvent -= CheckGrounded;
        }

        private void FixedUpdate()
        {
            _currentSuper.FixedUpdateStates();
        }

        private void Update()
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
                    Debug.Log("Hit climbable object");
                }
                else if (hit.transform.gameObject.CompareTag("Move_Object"))
                {
                    Debug.Log("Hit movable object");
                }
            }
        }

        private void SetRaycastVectors()
        {
            _halfVectors[0] = Vector3.down;
            _halfVectors[1] = Vector3.Lerp(Vector3.down, Vector3.back, _vectorAngle);
            _halfVectors[2] = Vector3.Lerp(Vector3.down, Vector3.forward, _vectorAngle);
            _halfVectors[3] = Vector3.Lerp(Vector3.down, Vector3.right, _vectorAngle);
            _halfVectors[4] = Vector3.Lerp(Vector3.down, Vector3.left, _vectorAngle);
            _halfVectors[5] = Vector3.Lerp(_halfVectors[2], _halfVectors[3], _vectorAngle);
            _halfVectors[6] = Vector3.Lerp(_halfVectors[2], _halfVectors[4], _vectorAngle);
            _halfVectors[7] = Vector3.Lerp(_halfVectors[1], _halfVectors[3], _vectorAngle);
            _halfVectors[8] = Vector3.Lerp(_halfVectors[1], _halfVectors[4], _vectorAngle);
        }

        private float testFloat;

        public void GroundedRaycast()
        {
            RaycastHit hit;
            for (int i = 0; i < _halfVectors.Length; i++)
            {
                Debug.DrawRay(transform.position, _halfVectors[i] * _rayGroundDist,
                        Color.red, 1);
                if (Physics.Raycast(transform.position, _halfVectors[i], out hit,
                    _rayGroundDist, _rayHitLayerMask))
                {
                    if (hit.distance > testFloat)
                    {
                        testFloat = hit.distance;
                    }
                    _isGrounded = true;
                    break;
                }

                //else if (i == _halfVectors.Length - 1)
                //{
                //    _isGrounded = false;
                //    Debug.Log("is happeing");
                //} Use if jump does not change IsGrounded
            }
        }
    }
}