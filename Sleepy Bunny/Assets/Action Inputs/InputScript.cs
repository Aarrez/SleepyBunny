using System;

using UnityEngine;
using UnityEngine.InputSystem;

// This code is in the statemachine script
public class InputScript : MonoBehaviour
{
    private ControllAction thePlayerInput;

    private ControllAction.CustomPlayerActions cPlayer;
    private ControllAction.CustomUIActions cUI;

    private InputAction.CallbackContext _moveCtx, _jumpCtx, _grabCtx,
            _pauseCtx, _crouchCtx;

    // Static so u easily can subscribe to these Actions.
    public static Action Moveing, Jump, Grab, Pause, Crouch, Climb;

    // This is for when you are using InputScript in another script.
    public InputAction.CallbackContext MoveCtx { get => _moveCtx; set => _moveCtx = value; }

    public InputAction.CallbackContext JumpCtx { get => _jumpCtx; set => _jumpCtx = value; }
    public InputAction.CallbackContext GrabCtx { get => _grabCtx; set => _grabCtx = value; }
    public InputAction.CallbackContext PauseCtx { get => _pauseCtx; set => _pauseCtx = value; }
    public InputAction.CallbackContext CrouchCtx { get => _crouchCtx; set => _crouchCtx = value; }

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
            MoveCtx = ctx;
        };

        cPlayer.Movement.canceled += ctx =>
        {
            Moveing?.Invoke();
            MoveCtx = ctx;
        };

        #endregion Movement Input

        #region Jump Input

        cPlayer.Jump.performed += ctx =>
        {
            Jump?.Invoke();
            JumpCtx = ctx;
        };

        #endregion Jump Input

        #region Grab Input

        cPlayer.Interact.performed += ctx =>
        {
            Grab?.Invoke();
            GrabCtx = ctx;
        };

        cPlayer.Interact.canceled += ctx =>
        {
            Grab?.Invoke();
            GrabCtx = ctx;
        };

        #endregion Grab Input

        #region Pause Input

        cUI.Pause.performed += ctx =>
        {
            Pause?.Invoke();
            _pauseCtx = ctx;
        };

        #endregion Pause Input

        #region Crouch Input

        cPlayer.Crouch.performed += ctx =>
        {
            Crouch?.Invoke();
            _crouchCtx = ctx;
        };

        cPlayer.Crouch.canceled += ctx =>
        {
            Crouch?.Invoke();
            _crouchCtx = ctx;
        };

        #endregion Crouch Input

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