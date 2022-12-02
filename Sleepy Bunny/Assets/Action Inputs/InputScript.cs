using System;

using UnityEngine;
using UnityEngine.InputSystem;

// This code is in the statemachine script
public class InputScript : MonoBehaviour
{
    private ControllAction thePlayerInput;

    private ControllAction.CustomPlayerActions cPlayer;

    private ControllAction.CustomUIActions cUI;

    private InputAction.CallbackContext _moveCtx, _jumpCtx, _interactCtx,
            _pauseCtx;

    // Static so u easily can subscribe to these Actions.
    public static Action Moveing, Jump, Interact, Pause, DebugState;

    public static event Action<InputAction.CallbackContext> MouseLeft, MouseRight;

    // This is for when you are using InputScript in another script.
    public InputAction.CallbackContext MoveCtx { get => _moveCtx; set => _moveCtx = value; }

    public InputAction.CallbackContext JumpCtx { get => _jumpCtx; set => _jumpCtx = value; }

    public InputAction.CallbackContext InteractCtx { get => _interactCtx; set => _interactCtx = value; }

    public InputAction.CallbackContext PauseCtx { get => _pauseCtx; set => _pauseCtx = value; }

    private void Awake()
    {
        thePlayerInput = new ControllAction();
        cPlayer = thePlayerInput.CustomPlayer;
        cUI = thePlayerInput.CustomUI;
    }

    private void OnEnable()
    {
        #region Input Stuff

        #region Movement Input

        cPlayer.Movement.performed += ctx =>
        {
            Moveing?.Invoke();
            _moveCtx = ctx;
        };

        cPlayer.Movement.canceled += ctx =>
        {
            Moveing?.Invoke();
            _moveCtx = ctx;
        };

        #endregion Movement Input

        #region Jump Input

        cPlayer.Jump.performed += ctx =>
        {
            Jump?.Invoke();
            _jumpCtx = ctx;
        };

        #endregion Jump Input

        #region Interact Input

        cPlayer.Interact.performed += ctx =>
        {
            Interact?.Invoke();
            _interactCtx = ctx;
        };

        //cPlayer.Interact.canceled += ctx =>
        //{
        //    Interact?.Invoke();
        //    _interactCtx = ctx;
        //};

        #endregion Interact Input

        #region Pause Input

        cUI.Pause.performed += ctx =>
        {
            Pause?.Invoke();
            _pauseCtx = ctx;
        };

        #endregion Pause Input

        #region Mouse Input

        cPlayer.LeftButtonMouse.performed += ctx =>
        {
            MouseLeft?.Invoke(ctx);
        };
        cPlayer.LeftButtonMouse.canceled += ctx =>
        {
            MouseLeft?.Invoke(ctx);
        };

        cPlayer.RightButtonMouse.performed += ctx =>
        {
            MouseRight?.Invoke(ctx);
        };
        cPlayer.RightButtonMouse.canceled += ctx =>
        {
            MouseRight?.Invoke(ctx);
        };

        #endregion Mouse Input

        #region Debug Input

        cPlayer.DebugState.performed += ctx => DebugState?.Invoke();

        #endregion Debug Input

        cPlayer.Enable();
        cUI.Enable();

        #endregion Input Stuff
    }

    private void OnDisable()
    {
        cPlayer.Disable();
        cUI.Disable();
    }
}