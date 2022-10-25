using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    #region Varibales

    private event Action doMove, doJump, doGrab, doPause;

    private Func<InputAction.CallbackContext> moveCtx, grabCtx, pauseCtx, jumpCtx;

    public float Speed = 1f;
    public float JumpHeight = 10f;
    public float RotationSpeed = .1f;
    public float Punchforce = 4f;
    public float Health;
    public float DamageAmount;
    private float climbSpeed = 5f;
    private float _amountOfHealth;

    public bool ShouldRespawn = false;

    public Vector3 Velocity;
    private Vector3 _movement = Vector3.zero;
    private Vector3 _movementDirection = Vector3.zero;

    public AudioSource PainNoise;
    public AudioSource Sizzle;

    private Rigidbody _rb;

    private GameMaster _gm;

    private ControllAction thePlayerInput;

    private ControllAction.CustomPlayerActions cPlayer;
    private ControllAction.CustomUIActions cUI;

    //State Variables
    private BasePlayerState _playerState;

    private StateFactory _stateFactory;

    public BasePlayerState PlayerState
    { get { return _playerState; } set { _playerState = value; } }

    #endregion Varibales

    private void Awake()
    {
        thePlayerInput = new ControllAction();
        cPlayer = thePlayerInput.CustomPlayer;
        cUI = thePlayerInput.CustomUI;

        //set state
        _stateFactory = new StateFactory(this);
        _playerState = _stateFactory.Grounded();
        _playerState.EnterState();
    }

    private void OnEnable()
    {
        #region Movement Input

        cPlayer.Movement.performed += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            moveCtx?.Invoke();
            doMove?.Invoke();
        };

        cPlayer.Movement.canceled += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            doMove?.Invoke();
        };

        #endregion Movement Input

        #region Jump Input

        cPlayer.Jump.performed += ctx => doJump?.Invoke();

        #endregion Jump Input

        #region Grab Input

        cPlayer.Grab.performed += ctx =>
        {
            grabCtx = delegate () { return ctx; };
            doGrab?.Invoke();
        };

        cPlayer.Grab.canceled += ctx =>
        {
            grabCtx = delegate () { return ctx; };
            doGrab?.Invoke();
        };

        #endregion Grab Input

        #region Pause Input

        cUI.Pause.performed += ctx =>
        {
            pauseCtx = delegate () { return ctx; };
            doPause?.Invoke();
        };

        #endregion Pause Input

        cPlayer.Enable();
        cUI.Enable();
    }

    private void OnDisable()
    {
        cPlayer.Disable();
        cUI.Disable();
    }
}