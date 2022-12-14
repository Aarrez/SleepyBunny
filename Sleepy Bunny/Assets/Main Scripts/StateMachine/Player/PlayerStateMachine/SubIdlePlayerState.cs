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
            if (Ctx.IsDead) { return; }
            if (Ctx.TheInput.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SwitchState(Factory.SubMovement());
            }
            else if (Ctx.Rb.velocity.y < -1f)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            if (Ctx.CurrentSuper == Factory.SuperJump()) { return; }
            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    Ctx.PlayerAnimator.SetFloat("IdleIndex",
                        (float)_eIdleAnim.IdleClimb);
                    break;

                case SuperGrabPlayerState:
                    if (Ctx.IsPulling)
                    {
                        Ctx.PlayerAnimator.SetFloat("IdleIndex",
                            (float)_eIdleAnim.IdlePull);
                    }
                    else if (Ctx.IsPushing)
                    {
                        Ctx.PlayerAnimator.SetFloat("IdleIndex",
                            (float)_eIdleAnim.IdlePush);
                    }
                    break;

                default:
                    Ctx.PlayerAnimator.SetFloat("IdleIndex", (float)_eIdleAnim.Idle);
                    break;
            }

            Ctx.PlayerAnimator.SetInteger("Index",
                (int)_eAnim.Idle);
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}