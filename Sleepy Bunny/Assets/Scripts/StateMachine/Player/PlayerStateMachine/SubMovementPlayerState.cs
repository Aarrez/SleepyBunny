using PlayerStM.BaseStates;
using PlayerStM.SuperState;
using UnityEngine;
using UnityEngine.iOS;

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
            if (Ctx.CurrentSuper == Factory.SuperJump()) { return; }

            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                        (float)_eMoveAnim.Climb); break;

                case SuperGrabPlayerState:
                    if (Ctx.IsPushing)
                    {
                        Ctx.PlayerAnimator.SetFloat("MoveIndex",
                        (float)_eMoveAnim.Push);
                    }
                    else if (Ctx.IsPulling)
                    {
                        Ctx.PlayerAnimator.SetFloat("MoveIndex",
                        (float)_eMoveAnim.Pull);
                    }
                    break;

                default:
                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                        (float)_eMoveAnim.Walk);
                    break;
            }
            Ctx.PlayerAnimator.SetInteger("Index",
                        (int)_eAnim.Walk);
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
            MoveCameraDirection();
            RotateToMovment();
            PlayerMove();
        }

        public override void UpdateState()
        {
        }

        private void PlayerMove()
        {
            // Player should not move while jumping
            if (Ctx.CurrentSuper == Factory.SuperJump()) { return; }

            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    PlayerClimb();
                    break;

                case SuperGrabPlayerState:
                    PullingMovement();
                    break;

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

        private void PullingMovement()
        {
            Ctx.Rb.velocity = MoveDirection * Ctx.MovmentForce / 2
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
    }
}