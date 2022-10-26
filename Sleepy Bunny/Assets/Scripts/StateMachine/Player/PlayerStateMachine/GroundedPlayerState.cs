using PlayerStM.BaseStates;
using UnityEngine.InputSystem;

namespace PlayerStM.SubStates
{
    public class GroundedPlayerState : BasePlayerState
    {
        private bool _hasJumped;

        public GroundedPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (_hasJumped)
            {
                SwitchState(StateFactory.Jump());
            }
        }

        public override void EnterState()
        {
            Ctx.Jump += HasJumped;
        }

        public override void ExitState()
        {
            Ctx.Jump -= HasJumped;
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        private void HasJumped(InputAction.CallbackContext inputCtx)
        {
            _hasJumped = inputCtx.ReadValueAsButton();
        }
    }
}