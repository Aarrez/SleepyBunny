using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStM.SubStates;

namespace PlayerStM.BaseStates
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region Refrences

        #region Script Refrences

        //Input value storage
        private Action<InputAction.CallbackContext> _moveing, _jump, _grab, _pause;

        public AudioSource PainNoise;
        public AudioSource Sizzle;

        private Rigidbody _rb;

        private GameMaster _gm;

        private ControllAction thePlayerInput;

        private ControllAction.CustomPlayerActions _cPlayer;
        private ControllAction.CustomUIActions _cUI;

        private Animator _anim;

        //State Variables
        private BasePlayerState _playerState;

        private StateFactory _stateFactory;

        #endregion Script Refrences

        #region Variables

        public Vector3 Velocity;
        private Vector3 _movement = Vector3.zero;
        private Vector3 _movementDirection = Vector3.zero;

        private float _force = 1f;
        private float jumpHeight = 10f;
        private float rotationSpeed = .1f;
        public float Punchforce = 4f;
        public float Health;
        public float DamageAmount;
        private float _climbSpeed = 5f;
        private float _amountOfHealth;

        public bool ShouldRespawn = false;
        private bool _isGrounded = false;
        private bool _isClimbing = false;

        #endregion Variables

        #endregion Refrences

        #region Get and set

        public Rigidbody Rb { get => _rb; set => _rb = value; }

        public Animator Anim { get => _anim; set => _anim = value; }

        public Action<InputAction.CallbackContext> Moveing
        {
            get => return _moveing;
            remove => _moveing -= value;
        }

        public event Action<InputAction.CallbackContext> Jump
        {
            add => _jump += value;
            remove => _jump -= value;
        }

        public event Action<InputAction.CallbackContext> Grab
        {
            add => _grab += value;
            remove => _grab -= value;
        }

        public event Action<InputAction.CallbackContext> Pause
        {
            add => _grab += value;
            remove => _grab -= value;
        }

        public BasePlayerState PlayerState
        { get { return _playerState; } set { _playerState = value; } }

        public float Force { get => _force; set => _force = value; }
        public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
        public bool IsClimbing { get => _isClimbing; set => _isClimbing = value; }

        #endregion Get and set

        private void Awake()
        {
            thePlayerInput = new ControllAction();
            _cPlayer = thePlayerInput.CustomPlayer;
            _cUI = thePlayerInput.CustomUI;

            _anim = GetComponent<Animator>();

            //set state
            _stateFactory = new StateFactory(this);
            _playerState = _stateFactory.Grounded();
            _playerState.EnterState();
        }

        private void OnEnable()
        {
            #region Input Stuff

            #region Movement Input

            _cPlayer.Movement.performed += ctx =>
            {
                _moveing?.Invoke(ctx);
            };

            _cPlayer.Movement.canceled += ctx =>
            {
                _moveing?.Invoke(ctx);
            };

            #endregion Movement Input

            #region Jump Input

            _cPlayer.Jump.performed += ctx => _jump?.Invoke(ctx);

            #endregion Jump Input

            #region Grab Input

            _cPlayer.Grab.performed += ctx =>
            {
                _grab?.Invoke(ctx);
            };

            _cPlayer.Grab.canceled += ctx =>
            {
                _grab?.Invoke(ctx);
            };

            #endregion Grab Input

            #region Pause Input

            _cUI.Pause.performed += ctx =>
            {
                _pause?.Invoke(ctx);
            };

            #endregion Pause Input

            _cPlayer.Enable();
            _cUI.Enable();

            #endregion Input Stuff

            Grounded.IsGroundedEvent += CheckGrounded;
        }

        private void OnDisable()
        {
            _cPlayer.Disable();
            _cUI.Disable();

            Grounded.IsGroundedEvent += CheckGrounded;
        }

        private void FixedUpdate()
        {
            _playerState.UpdateState();
        }

        private void CheckGrounded(bool grounded)
        {
            _isGrounded = grounded;
        }
    }
}