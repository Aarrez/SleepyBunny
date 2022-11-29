using PlayerStM.BaseStates;
using PlayerStM.SuperState;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// This state handles all functionaly when the player is falling
    /// </summary>
    /// If your want more info then please refer to TemplatePlayerState.cs

    public class SubFallingPlayerState : BasePlayerState
    {
        public SubFallingPlayerState
            (
            PlayerStateMachine currentContext
            , StateFactory stateFactory
            )
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (!Ctx.IsGrounded) { return; }

            SwitchState(Factory.SubIdle());
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetInteger("Index", (int)_eAnim.Falling);
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        public override void UpdateState()
        {
            Ctx.GroundedRaycast();
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
    }
}