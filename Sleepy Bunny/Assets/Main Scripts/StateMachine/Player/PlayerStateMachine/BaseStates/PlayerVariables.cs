using System.Collections;
using System.Collections.Generic;
using PlayerStM.SubStates;
using UnityEditor;
using UnityEngine;

namespace PlayerStM.BaseStates
{
    public class PlayerVariables : MonoBehaviour
    {
        #region Script Refrences

        internal OtherGrab _otherGrab;

        internal InputScript _theInput;

        //Input value storage

        internal Rigidbody _rb;

        internal RaycastHit hit;

        internal GameMaster _gameMaster;

        internal ControllAction thePlayerInput;

        internal Camera _mainCamera;

        //State Variables
        internal BasePlayerState _currentSuper;

        internal BasePlayerState _currentSub;

        internal StateFactory _stateFactory;

        #endregion Script Refrences

        #region Serialized Variables

        [Header("Physics vaiables")]
        [Tooltip("This velocity dictates when to switch from Land soft animaiton" +
            "\n to land hard animation.")]
        [SerializeField] internal float _softHitVelocity = -3.0f;

        [Tooltip("When charcter hits this or below this velocity it dies.")]
        [SerializeField] internal float _deadVelocity = -6.0f;

        [Tooltip("The default gravity setting is -9.82." +
            "The gravity modifier adds or subtracts from that number")]
        [SerializeField] internal float _gravityModifier = 0f;

        [Header("Movement related variables")]
        [Tooltip("Determines how speedy the character is")]
        [SerializeField] internal float _movmentForce = 1f;

        [Tooltip("Is multiplied with MovemetnForce to get running force")]
        [SerializeField] internal float _runningModifier = 1.5f;

        [Header("Jump variables")]

        //
        [Tooltip("If true there the charcter will be able to move mid air. " +
            "\nHow fast it moves is determined by JumpMovementMultipler" +
            "\nIf false player uses DirectionalJumpForce at the start of the jump to" +
            "determine fly forward with no air conroll")]
        [SerializeField] internal bool _airMovement;

        [Tooltip("Multipler for jump movemnt")]
        [SerializeField] internal float _jumpMovementMultipler = 0.5f;

        [Tooltip("Determines how much directional force is applyed when jumping")]
        [SerializeField] internal float _directionalJumpForce = .5f;

        [Tooltip("Determines how high you jump")]
        [SerializeField] internal float _jumpHeight = 10f;

        [Header("")]

        //
        [Tooltip("Character snaps to object at this distance")]
        [SerializeField] internal float _hitDistanceModifier = 0.1f;

        [SerializeField] internal float _climbSpeed = 5f;

        [Tooltip("Changes how fast the character turns around")]
        [SerializeField] internal float _rotationSpeed;

        [Header("Raycast to ground variables")]

        //
        [Tooltip("How long the ray is that checks for ground" +
            "\n" + "(Should be 0.1 unless debuging)")]
        [SerializeField] internal float _rayGroundDist = 0.1f;

        [Tooltip("Changes the angle in which the cone of rays are shot" +
            "\n" + "0 = 0 degres(Strait down), 1 = 90 degres")]
        [SerializeField, Range(0, 1)] internal float _vectorAngle = 0.5f;

        [Tooltip("Changes the angle between forward ray and the rays around it" +
            "\n" + "0 = 0 degres(Strait forward), 1 = 90 degres")]
        [SerializeField, Range(0, 1)] internal float _forwardVAngel = 0.3f;

        [Tooltip("If true the uses already set layers." + "\n"
            + "If false you can set custom layers")]
        [SerializeField] internal bool _useDefaultGroundLayer = true;

        [Tooltip("Only change if debuging!" + "\n" +
           "Inital layermask is Default and Ground")]
        [SerializeField] internal LayerMask _groundLayer;

        [Header("Other raycast variables")]

        //
        [Tooltip("If true the uses already set layers." + "\n"
            + "If false you can set custom layers")]
        [SerializeField] internal bool _useDefaultInteractionsLayers = true;

        [Tooltip("The layer the climb ray will hit." +
            "\n" + "Will automaticly have Climbable layer")]
        [SerializeField] internal LayerMask _climbLayer;

        [Tooltip("Default 0.5")]
        [SerializeField] internal float _climbRayLength = 0.5f;

        [Tooltip("The layer grab ray will hit." +
            "\n" + "Will automaticly have Grabable layer")]
        [SerializeField] internal LayerMask _grabLayer;

        [Tooltip("Default 1")]
        [SerializeField] internal float _grabRayLength = 1f;

        [SerializeField] internal float _pullForce = 2f;

        [SerializeField] internal float _pushForce = 2f;

        [Tooltip("The layer in witch rays do the interact functionality." +
            "\n" + "Will automaticly have Interactable layer")]
        [SerializeField] internal LayerMask _interactLayer;

        [Tooltip("Default 1")]
        [SerializeField] internal float _interactRayLength = 1f;

        [Header("Push and pull variables")]
        [SerializeField] internal float _breakDistance = 3f;

        [SerializeField] internal float _pullDistance = 0.5f;

        #endregion Serialized Variables

        #region Private Variables

        internal const float _gravity = -9.82f;

        internal Transform _pointHit;

        internal GameObject hitPoint;

        internal List<Vector3> _downVectors = new List<Vector3>();

        internal List<Vector3> _forwardVector = new List<Vector3>();

        internal Animator _playerAnimator;

        internal bool _isGrounded = false;

        internal bool _isClimbing = false;

        internal bool _isFalling = false;

        internal bool _isGrabing = false;

        internal bool _isPushing = false;

        internal bool _isPulling = false;

        internal bool _isDead = false;

        internal bool _landAnimationDone = false;

        internal bool _reachedEdge = false;

        internal Transform _transformHit;

        internal Rigidbody _rigidbodyGrabed;

        #endregion Private Variables

        #region Get and set

        //Miscellaneous Get and set

        public float Gravity
        {
            get { return _gravity; }
        }

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

        public GameMaster GameMaster => _gameMaster;

        public List<Vector3> ForwardVector => _forwardVector;

        // Get and set floats

        public float MovmentForce
        {
            get => _movmentForce;
            set => _movmentForce = value;
        }

        public float RunningModifier
        {
            get => _runningModifier;
            set => _runningModifier = value;
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

        public float JumpMovementMultipler
        {
            get => _jumpMovementMultipler;
            set => _jumpMovementMultipler = value;
        }

        public float ClimbRayLength => _climbRayLength;

        public float RotationSpeed => _rotationSpeed;

        public float ClimbSpeed => _climbSpeed;

        public float SoftHitVelocity => _softHitVelocity;

        public float DeadVelocity => _deadVelocity;

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

        public bool AirMovement
        {
            get => _airMovement;
            set => _airMovement = value;
        }

        public bool ReachedEdge
        {
            get => _reachedEdge;
            set => _reachedEdge = value;
        }

        public bool IsFalling => _isFalling;

        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
        }

        public Camera MainCamera => _mainCamera;

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

        #endregion Get and set
    }
}