using PlayerStM.BaseStates;
using PlayerStM.SuperState;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// Handles everything with movement so every superstate has this
    /// substate if it involves movment
    /// </summary>
    ///
    public class SubMovementPlayerState : BasePlayerState
    {
        public SubMovementPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.MoveCtx.ReadValue<Vector2>() == Vector2.zero)
            {
                SwitchState(Factory.SubIdle());
            }
            else if (Ctx.Rb.velocity.y > 0)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetFloat("GSIndex",
                        (float)_eGroundAnim.Walking);
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
            PlayerMove();
            RotateToMovment();
        }

        private void PlayerMove()
        {
            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState: break;

                case SuperPushingPlayerState: break;

                // SuperJump should always be blank so no movment
                // can be done when jumping
                case SuperJumpPlayerState: break;

                default:
                    GroundedMovment();
                    break;
            }
        }

        private void GroundedMovment()
        {
            Ctx.Rb.velocity = MoveDirection * Ctx.MovmentForce
                * Time.fixedDeltaTime;
        }

        private void RotateToMovment()
        {
            if (MoveDirection == Vector3.zero) return;

            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
                Quaternion.LookRotation(MoveDirection, Vector3.up),
                Ctx.RotationSpeed);
        }
    }
}