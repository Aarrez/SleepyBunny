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
                SwitchState(StateFactory.Grounded());
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
            InputAction.CallbackContext some = Ctx.Moveing();
            if (Ctx.Moveing)
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