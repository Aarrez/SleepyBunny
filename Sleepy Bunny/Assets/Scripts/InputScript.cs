using System;
using System.Globalization;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private ControllAction thePlayerInput;

    private ControllAction.CustomPlayerActions cPlayer;
    private ControllAction.CustomUIActions cUI;

    public static event Action doMove, doJump, doGrab, doPause;

    public static Func<InputAction.CallbackContext> moveCtx, grabCtx, pauseCtx;

    private void Awake()
    {
        thePlayerInput = new ControllAction();
        cPlayer = thePlayerInput.CustomPlayer;
        cUI = thePlayerInput.CustomUI;
    }

    private void OnEnable()
    {
        #region Movement Input

        cPlayer.Movement.performed += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            doMove?.Invoke();
        };

        cPlayer.Movement.canceled += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            doMove?.Invoke();
        };

        cPlayer.Movement.Enable();

        #endregion Movement Input

        #region Jump Input

        cPlayer.Jump.performed += ctx => doJump?.Invoke();

        cPlayer.Jump.Disable();

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
            doGrab.Invoke();
        };

        cPlayer.Grab.Enable();

        #endregion Grab Input

        #region Pause Input

        cUI.Pause.started += ctx =>
        {
            pauseCtx = delegate () { return ctx; };
            doPause?.Invoke();
        };

        cUI.Pause.Enable();

        #endregion Pause Input
    }

    private void OnDisable()
    {
        cPlayer.Movement.Disable();
        cPlayer.Jump.Disable();
        cPlayer.Grab.Disable();
        cUI.Pause.Disable();
    }
}