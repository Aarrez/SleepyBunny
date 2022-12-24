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
        public SubIdlePlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods
            , StateFactory factrory)
            : base(variables, methods, factrory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Variables.IsDead) { return; }
            if (Variables.TheInput.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SwitchState(Factory.SubMovement());
            }
            else if (Variables.Rb.velocity.y < -1f)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            if (Variables.CurrentSuper == Factory.SuperJump()) { return; }
            switch (Variables.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    Variables.PlayerAnimator.SetFloat("IdleIndex",
                        (float)_eIdleAnim.IdleClimb);
                    break;

                case SuperGrabPlayerState:
                    if (Variables.IsPulling)
                    {
                        Variables.PlayerAnimator.SetFloat("IdleIndex",
                            (float)_eIdleAnim.IdlePull);
                    }
                    else if (Variables.IsPushing)
                    {
                        Variables.PlayerAnimator.SetFloat("IdleIndex",
                            (float)_eIdleAnim.IdlePush);
                    }
                    break;

                default:
                    Variables.PlayerAnimator.SetFloat("IdleIndex", (float)_eIdleAnim.Idle);
                    break;
            }

            Variables.PlayerAnimator.SetInteger("Index",
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