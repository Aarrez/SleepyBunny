using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private ControllAction.PlayerActions playerInput;

    public static event Action DoMove;

    public static Func<InputAction.CallbackContext> MoveCtx;

    private void Awake()
    {
        playerInput = new ControllAction.PlayerActions();

        playerInput.Movement.performed += ctx =>
        {
            MoveCtx = delegate () { return ctx; };
            DoMove?.Invoke();
        };

        playerInput.Movement.canceled += ctx =>
        {
            MoveCtx = delegate () { return ctx; };
            DoMove?.Invoke();
        };
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}