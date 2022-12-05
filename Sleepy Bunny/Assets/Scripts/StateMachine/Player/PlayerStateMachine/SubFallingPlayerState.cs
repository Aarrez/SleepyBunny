using PlayerStM.BaseStates;
using PlayerStM.SuperState;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
            if (!Ctx.IsGrounded || Ctx.IsClimbing) { return; }

            SwitchState(Factory.SubIdle());
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.ResetTrigger("Landed");
            Ctx.PlayerAnimator.SetInteger("Index", (int)_eAnim.Falling);
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
            Ctx.GroundedRaycast();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            if (Ctx.CurrentSuper != Factory.SuperClimb())
            {
                Ctx.PlayerAnimator.SetTrigger("Landed");
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}