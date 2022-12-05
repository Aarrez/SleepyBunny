using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerStM.SubStates;
using Unity.VisualScripting;

namespace PlayerStM.BaseStates
{
    /// <summary>
    /// This is the Main class of the player state machine where all the magic happens.
    /// <br></br>
    /// If you want more info refer to
    /// <see href="https://docs.google.com/document/d/1aFpvd6ApL2zObopLuXXM9AEezrWgWXJt30wbKSJnSlc/edit?usp=sharing">
    /// Programming Design Doc
    /// </see>.
    /// <br></br>
    /// StateFactory.cs and BasePlayerState.cs are the other two classes that's important
    /// </summary>
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Script Refrences

        private OtherGrab _otherGrab;

        private InputScript _theInput;

        //Input value storage

        private Rigidbody _rb;

        private RaycastHit hit;

        private GameMaster _gm;

        private ControllAction thePlayerInput;

        private Camera _mainCamera;

        //State Variables
        private BasePlayerState _currentSuper;

        private BasePlayerState _currentSub;

        private StateFactory _stateFactory;

        #endregion Script Refrences

        #region Serialized Variables

        [Header("Physics vaiables")]
        [Tooltip("This velocity dictates when to switch from Land soft animaiton" +
            "\n to land hard animation.")]
        [SerializeField] private float _softHitVelocity = -3.0f;

        [Tooltip("The default gravity setting is -9.82." +
            "The gravity modifier adds or subtracts from that number")]
        [SerializeField] private float _gravityModifier = 0f;

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

        //
        [Tooltip("How long the ray is that checks for ground" +
            "\n" + "(Should be 0.1 unless debuging)")]
        [SerializeField] private float _rayGroundDist = 0.1f;

        [Tooltip("Changes the angle in which the cone of rays are shot" +
            "\n" + "0 = 0 degres(Strait down), 1 = 90 degres")]
        [SerializeField, Range(0, 1)] private float _vectorAngle = 0.5f;

        [Tooltip("Changes the angle between forward ray and the rays around it" +
            "\n" + "0 = 0 degres(Strait forward), 1 = 90 degres")]
        [SerializeField, Range(0, 1)] private float _forwardVAngel = 0.3f;

        [Tooltip("If true the uses already set layers." + "\n"
            + "If false you can set custom layers")]
        [SerializeField] private bool _useDefaultGroundLayer = true;

        [Tooltip("Only change if debuging!" + "\n" +
           "Inital layermask is Default and Ground")]
        [SerializeField] private LayerMask _groundLayer;

        [Header("Other raycast variables")]

        //
        [Tooltip("If true the uses already set layers." + "\n"
            + "If false you can set custom layers")]
        [SerializeField] private bool _useDefaultInteractionsLayers = true;

        [Tooltip("The layer the climb ray will hit." +
            "\n" + "Will automaticly have Climbable layer")]
        [SerializeField] private LayerMask _climbLayer;

        [Tooltip("Default 0.5")]
        [SerializeField] private float _climbRayLength = 0.5f;

        [Tooltip("The layer grab ray will hit." +
            "\n" + "Will automaticly have Grabable layer")]
        [SerializeField] private LayerMask _grabLayer;

        [Tooltip("Default 1")]
        [SerializeField] private float _grabRayLength = 1f;

        [SerializeField] private float _pullForce = 2f;

        [SerializeField] private float _pushForce = 2f;

        [Tooltip("The layer in witch rays do the interact functionality." +
            "\n" + "Will automaticly have Interactable layer")]
        [SerializeField] private LayerMask _interactLayer;

        [Tooltip("Default 1")]
        [SerializeField] private float _interactRayLength = 1f;

        [Header("Push and pull variables")]
        [SerializeField] private float _breakDistance = 3f;

        [SerializeField] private float _pullDistance = 0.5f;

        #endregion Serialized Variables

        #region Private Variables

        private const float _gravity = -9.82f;

        private Transform _pointHit;

        private List<Vector3> _downVectors = new List<Vector3>();

        private List<Vector3> _forwardVector = new List<Vector3>();

        private Animator _playerAnimator;

        private bool _isGrounded = false;

        private bool _isClimbing = false;

        private bool _isFalling = false;

        private bool _isGrabing = false;

        private bool _isPushing = false;

        private bool _isPulling = false;

        private bool _landAnimationDone = false;

        private Transform _transformHit;

        private Rigidbody _rigidbodyGrabed;

        #endregion Private Variables

        #region Get and set

        //Miscellaneous Get and set
        public Transform TransformHit
        {
            get => _transformHit;
            set => _transformHit = value;
        }

        public Transform PointHit
        {
            get => _pointHit;
            set => _pointHit = value;
        }

        public LayerMask GroundLayer => _groundLayer;

        public LayerMask ClimbLayer => _climbLayer;

        public LayerMask GrabLayer => _grabLayer;

        public InputScript TheInput => _theInput;

        public Rigidbody Rb
        {
            get => _rb;
            set => _rb = value;
        }

        public Rigidbody RigidbodyGrabed
        {
            get => _rigidbodyGrabed;
            set => _rigidbodyGrabed = value;
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

        // Get and set floats

        public float MovmentForce
        {
            get => _movmentForce;
            set => _movmentForce = value;
        }

        public float JumpHeight
        {
            get => _jumpHeight;
            set => _jumpHeight = value;
        }

        public float DirectionalJumpForce
        {
            get => _directionalJumpForce;
            set => _directionalJumpForce = value;
        }

        public float PullForce
        {
            get => _pullForce;
            set => _pullForce = value;
        }

        public float PushForce
        {
            get => _pushForce;
            set => _pushForce = value;
        }

        public float BreakDistance
        {
            get => _breakDistance;
            set => _breakDistance = value;
        }

        public float PullDistance
        {
            get => _pullDistance;
            set => _pullDistance = value;
        }

        public float ClimbRayLength => _climbRayLength;

        public float RotationSpeed => _rotationSpeed;

        public float ClimbSpeed => _climbSpeed;

        public float SoftHitVelocity => _softHitVelocity;

        //Get and set bools
        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public bool IsClimbing
        {
            get => _isClimbing;
            set => _isClimbing = value;
        }

        public bool IsGrabing
        {
            get => _isGrabing;
            set => _isGrabing = value;
        }

        public bool IsPushing
        {
            get => _isPushing;
            set => _isPushing = value;
        }

        public bool IsPulling
        {
            get => _isPulling;
            set => _isPulling = value;
        }

        public bool LandAnimationDone
        {
            get => _landAnimationDone;
            set => _landAnimationDone = value;
        }

        public bool IsFalling => _isFalling;

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

            _playerAnimator = GetComponentInChildren<Animator>();
            _rb = GetComponentInParent<Rigidbody>();

            _stateFactory = new StateFactory(this);
            _currentSuper = _stateFactory.SuperGrounded();
            _currentSuper.EnterState();

            _mainCamera = Camera.main;
        }

        private void Start()
        {
            SetRaycastVectors();

            Physics.gravity = new Vector3(0, _gravity + _gravityModifier, 0);
        }

        private void OnValidate()
        {
            if (_useDefaultGroundLayer)
            {
                _groundLayer = LayerMask.GetMask("Ground") + LayerMask.GetMask("Default");
            }

            if (_useDefaultInteractionsLayers)
            {
                _climbLayer = LayerMask.GetMask("Climbable");
                _grabLayer = LayerMask.GetMask("Grabable");
                _interactLayer = LayerMask.GetMask("Interactable");
            }
        }

        private void OnEnable()
        {
            InputScript.DebugState += GetCurrentState;

            InputScript.Interact += ClimbGrabInteract;

            Grounded.IsGroundedEvent += GroundedRaycast;
        }

        private void OnDisable()
        {
            InputScript.DebugState -= GetCurrentState;

            InputScript.Interact -= ClimbGrabInteract;

            Grounded.IsGroundedEvent -= GroundedRaycast;
        }

        private void FixedUpdate()
        {
            _currentSuper.FixedUpdateStates();
        }

        private void Update()
        {
            _currentSuper.UpdateStates();
        }

        /// <summary>
        /// Not in use. Old method for checking if something is grounded.
        /// <br></br>
        /// Does not work
        /// </summary>
        /// <param name="grounded"></param>
        private void CheckGrounded(bool grounded)
        {
            _isGrounded = grounded;
        }

        /// <summary>
        /// Press the Select(View on xbox controller) button on the gamepad
        /// <br></br>
        /// to see the current super- and substate in the console
        /// </summary>
        private void GetCurrentState()
        {
            Debug.Log("SuperState " + CurrentSuper);
            Debug.Log("SubState: " + CurrentSub);
        }

        private void SetRaycastVectors()
        {
            // Cardinal directions are relative to the players rotation
            //
            //0
            _downVectors.Add(Vector3.down);

            //1 south
            _downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.back, _vectorAngle));

            //2 north
            _downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.forward, _vectorAngle));

            //3 east
            _downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.right, _vectorAngle));

            //4 west
            _downVectors.Add(Vector3.Lerp
                (Vector3.down, Vector3.left, _vectorAngle));

            //5 north east
            _downVectors.Add(Vector3.Lerp
                (_downVectors[2], _downVectors[3], _vectorAngle));

            //6 north west
            _downVectors.Add(Vector3.Lerp
                (_downVectors[2], _downVectors[4], _vectorAngle));

            //7 south east
            _downVectors.Add(Vector3.Lerp
                (_downVectors[1], _downVectors[3], _vectorAngle));

            //8 south west
            _downVectors.Add(Vector3.Lerp
                (_downVectors[1], _downVectors[4], _vectorAngle));

            _forwardVector.Add(Vector3.forward);

            _forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.up, _forwardVAngel));

            _forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.down, _forwardVAngel));

            _forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.right, _forwardVAngel));

            _forwardVector.Add(Vector3.Lerp
                (Vector3.forward, Vector3.left, _forwardVAngel));
        }

        /// <summary>
        /// Shoots out Rays and that hits specific Layers.
        /// <br></br>
        /// The layers can be specified in the editor under PlayerStateMachine
        /// </summary>
        public void ClimbGrabInteract()
        {
            if (_isGrabing || _isClimbing)
            {
                _isGrabing = false;
                _isClimbing = false;
                _isPulling = false;
                _isPushing = false;
                hit.IsUnityNull();

                return;
            }

            // The forloop shoots out rays in all direction unitl
            // one hits a object with the correct layer
            for (int i = 0; i < _forwardVector.Count; i++)
            {
                Vector3 tempVector =
                    Camera.main.transform.TransformDirection(_forwardVector[i]);

                Debug.DrawRay(transform.position, tempVector * 1, Color.green, 2);

                //Climb ray
                if (Physics.Raycast(transform.position, tempVector, out hit,
                _climbRayLength, _climbLayer))
                {
                    _transformHit = hit.transform;

                    transform.rotation = Quaternion.RotateTowards(transform.rotation,
                        _transformHit.rotation, _rotationSpeed);
                    _isClimbing = true;
                    break;
                }

                //Grab ray
                else if (Physics.Raycast(transform.position, tempVector, out hit,
                    _climbRayLength, _grabLayer))
                {
                    RayGrab();
                    break;
                }

                // Interaction Ray
                else if (Physics.Raycast(transform.position, tempVector, out hit,
                    _interactRayLength, _interactLayer))
                {
                    try
                    {
                        hit.transform.gameObject.GetComponent<InteractObject>()
                            .Interacted.Invoke();
                    }
                    catch (Exception)
                    {
                        hit.transform.gameObject.GetComponentInChildren<InteractObject>()
                            .Interacted.Invoke();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Shoots a multitude of rays down in a cone that
        /// <br></br>
        /// checks for a object with the grounded layer
        /// </summary>
        public void GroundedRaycast()
        {
            RaycastHit hit;
            for (int i = 0; i < _downVectors.Count; i++)
            {
                Debug.DrawRay(transform.position, _downVectors[i] * _rayGroundDist,
                        Color.red, 1);
                if (Physics.Raycast(transform.position, _downVectors[i],
                    _rayGroundDist, _groundLayer, QueryTriggerInteraction.Collide))
                {
                    _isGrounded = true;
                    break;
                }
                else if (i == _downVectors.Count - 1)
                {
                    _isGrounded = false;
                }
            }
        }

        private void RayGrab()
        {
            float distance =
                        Vector3.Distance(transform.position, hit.transform.position);

            _transformHit = hit.transform;
            _rigidbodyGrabed = hit.transform.GetComponent<Rigidbody>();

            GameObject hitPoint = new GameObject();
            hitPoint.transform.position = hit.point;
            hitPoint.transform.parent = hit.transform;
            _pointHit = hitPoint.transform;

            if (distance > _pullDistance)
            {
                _isPushing = true;
            }
            else if (distance < _pullDistance)
            {
                _isPulling = true;
            }

            _isGrabing = true;
        }
    }
}