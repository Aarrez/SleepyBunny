using PlayerStM.BaseStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerStM.SuperState
{
    public class JumpPlayerState : BasePlayerState
    {
        public JumpPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.Grounded());
            }
        }

        public override void EnterState()
        {
            Ctx.Jump += DoJump;
        }

        public override void ExitState()
        {
            Ctx.Jump -= DoJump;
        }

        public override void InitializeSubState()
        {
            if (Ctx.IsClimbing && Ctx.IsFalling)
            {
                SetSubState(Factory.Climb());
            }
            else if (Ctx.IsFalling)
            {
                SetSubState(Factory.Falling());
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        private void DoJump(InputAction.CallbackContext inputCtx)
        {
            Ctx.Rb.AddForce(Vector3.up * Ctx.JumpHeight, ForceMode.Impulse);
        }
    }
}