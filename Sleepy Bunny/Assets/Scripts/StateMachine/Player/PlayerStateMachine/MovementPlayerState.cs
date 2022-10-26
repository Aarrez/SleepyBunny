using PlayerStM.BaseStates;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerStM.SubStates
{
    public class MovementPlayerState : BasePlayerState
    {
        private Vector3 _moveVector;

        public MovementPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (_moveVector == Vector3.zero)
            {
                SwitchState(StateFactory.Idle());
            }
            else if (!Ctx.IsGrounded)
            {
                SwitchState(StateFactory.Falling());
            }
            else if (Ctx.IsClimbing)
            {
                SwitchState(StateFactory.Climb());
            }
        }

        public override void EnterState()
        {
            Ctx.Moveing += GetMoveContext;
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveContext;
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
            PlayerMoving();
        }

        private void GetMoveContext(InputAction.CallbackContext ctx)
        { _moveVector = new Vector3(ctx.ReadValue<Vector2>().x, 0f, ctx.ReadValue<Vector2>().y); }

        private void PlayerMoving()
        {
            Ctx.Rb.AddForce(_moveVector * Ctx.Force);
        }
    }
}