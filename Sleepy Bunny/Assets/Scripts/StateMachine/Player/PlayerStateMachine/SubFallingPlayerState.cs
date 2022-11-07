using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SubFallingPlayerState : BasePlayerState
    {
        public SubFallingPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SubIdle());
            }
            if (Ctx.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SwitchState(Factory.SubMovement());
            }
        }

        public override void EnterState()
        {
            Debug.Log("Enterd falling");
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}