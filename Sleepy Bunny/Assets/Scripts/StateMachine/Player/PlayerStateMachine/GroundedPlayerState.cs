using PlayerStM.BaseStates;
using UnityEngine.InputSystem;

namespace PlayerStM.SuperState
{
    public class GroundedPlayerState : BasePlayerState
    {
        private InputAction.CallbackContext _moveCtx;

        private bool _hasJumped;

        public GroundedPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (_hasJumped)
            {
                SwitchState(Factory.Jump());
            }
        }

        public override void EnterState()
        {
            Ctx.Jump += HasJumped;
            Ctx.Moveing += MoveInput;
        }

        public override void ExitState()
        {
            Ctx.Jump -= HasJumped;
            Ctx.Moveing -= MoveInput;
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

        private void MoveInput(InputAction.CallbackContext inputCtx)
        {
            _moveCtx = inputCtx;
        }
    }
}