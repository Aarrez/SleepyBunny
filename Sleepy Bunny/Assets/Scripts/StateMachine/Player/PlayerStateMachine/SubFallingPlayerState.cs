using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// This state handles all functionaly when the player is falling
    /// </summary>
    /// If your want more info then please refer to TemplatePlayerState.cs

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