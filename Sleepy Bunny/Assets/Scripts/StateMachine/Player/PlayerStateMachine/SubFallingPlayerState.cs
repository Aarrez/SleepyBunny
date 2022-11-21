using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
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
            if (!Ctx.IsGrounded) { return; }

            SwitchState(Factory.SubLand());
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetFloat("GSIndex",
                (float)_eGroundAnim.Falling);
        }

        public override void UpdateState()
        {
            CheckSwitchState();
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