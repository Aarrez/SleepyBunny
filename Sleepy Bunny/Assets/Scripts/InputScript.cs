using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private ControllAction thePlayerInput;

    private InputAction move, mouse;

    public static event Action doMove, doMouse;

    public static Func<InputAction.CallbackContext> moveCtx, mouseCtx;

    private void Awake()
    {
        thePlayerInput = new ControllAction();
        move = thePlayerInput.Player.Movement;
        mouse = thePlayerInput.Player.MouseInput;
    }

    private void OnEnable()
    {
        #region Movement Input

        move.performed += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            doMove?.Invoke();
            Debug.Log(move.WasPerformedThisFrame());
        };

        move.canceled += ctx =>
        {
            moveCtx = delegate () { return ctx; };
            doMove?.Invoke();
        };

        #endregion Movement Input

        #region Mouse Input

        mouse.performed += ctx =>
        {
            mouseCtx = delegate () { return ctx; };
            doMouse?.Invoke();
        };

        #endregion Mouse Input

        move.Enable();
        mouse.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        mouse.Disable();
    }
}