using UnityEngine;
using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// This class is most defenetly the default substate
    /// becase it handles everything todo with statding still.
    /// </summary>
    public class SubIdlePlayerState : BasePlayerState
    {
        public SubIdlePlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SwitchState(Factory.SubMovement());
            }
            else if (Ctx.Rb.velocity.y < 0f)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetFloat("AnimIndex",
                (float)_eAnim.Idle);
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