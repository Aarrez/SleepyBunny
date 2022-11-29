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
            if (Ctx.TheInput.MoveCtx.ReadValue<Vector2>() == Vector2.zero)
            {
                SwitchState(Factory.SubIdle());
            }
            else if (Ctx.Rb.velocity.y < -1f)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetInteger("Index",
                        (int)_eAnim.Walk);
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

        public override void FixedUpdateState()
        {
            CheckSwitchState();
            PlayerMove();
            RotateToMovment();
        }

        private void PlayerMove()
        {
            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    PlayerClimb();
                    break;

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
            Debug.Log("Movement should be happeing");
            Ctx.Rb.velocity = MoveDirection * Ctx.MovmentForce
                * Time.fixedDeltaTime;
        }

        private void PlayerClimb()
        {
            Ctx.Rb.velocity = MoveVector * Ctx.ClimbSpeed
                * Time.fixedDeltaTime;
        }

        private void RotateToMovment()
        {
            if (MoveDirection == Vector3.zero) return;

            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
                Quaternion.LookRotation(MoveDirection, Vector3.up),
                Ctx.RotationSpeed);
        }

        public override void UpdateState()
        {
        }
    }
}