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
            if (Ctx.CurrentSuper == Factory.SuperJump()) { return; }

            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                        (float)_eMoveAnim.Climb);
                    break;

                case SuperGrabPlayerState:

                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                    (float)_eMoveAnim.Pull);

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
            PlayerMove();
        }

        public override void UpdateState()
        {
        }

        private void PlayerMove()
        {
            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    PlayerClimb();
                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                       (float)_eMoveAnim.Climb);
                    break;

                case SuperGrabPlayerState:
                    InverseRotateToMovment();
                    PullingMovement();
                    Ctx.PlayerAnimator.SetFloat("MoveIndex",
                    (float)_eMoveAnim.Pull);
                    break;

                case SuperJumpPlayerState:
                    if (Ctx.AirMovement)
                    {
                        JumpMovemet();
                    }

                    break;

                default:
                    RotateToMovment();
                    if (Ctx.TheInput.RunningCtx.ReadValueAsButton())
                    {
                        RunningMovement();
                        Ctx.PlayerAnimator.SetFloat("MoveIndex",
                            (float)_eMoveAnim.Run);
                    }
                    else
                    {
                        GroundedMovment();
                        Ctx.PlayerAnimator.SetFloat("MoveIndex",
                            (float)_eMoveAnim.Walk);
                    }

                    break;
            }
        }

        private void GroundedMovment()
        {
            Vector3 Movement = MoveDirection * Ctx.MovmentForce * Time.fixedDeltaTime;

            Ctx.Rb.velocity = new Vector3(
                Movement.x,
                Ctx.Rb.velocity.y,
                Movement.z);
        }

        private void RunningMovement()
        {
            Vector3 Movement = MoveDirection *
                Ctx.MovmentForce * Ctx.RunningModifier * Time.fixedDeltaTime;

            Ctx.Rb.velocity = new Vector3(
                Movement.x,
                Ctx.Rb.velocity.y,
                Movement.z);
        }

        internal void JumpMovemet()
        {
            Vector3 Movement = MoveDirection *
                (Ctx.MovmentForce * Ctx.JumpMovementMultipler) * Time.fixedDeltaTime;
            Ctx.Rb.velocity = new Vector3(
                Movement.x,
                Ctx.Rb.velocity.y,
                Movement.z);
        }

        private void PullingMovement()
        {
            Vector3 Movement = MoveDirection * Ctx.PullForce * Time.fixedDeltaTime;
            Ctx.Rb.AddForce(Movement);

            //Ctx.Rb.velocity = new Vector3(
            //    Movement.x,
            //    Ctx.Rb.velocity.y,
            //    Movement.z);
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

        private void InverseRotateToMovment()
        {
            if (MoveDirection == Vector3.zero) return;

            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
             Quaternion.LookRotation(-MoveDirection, Vector3.up),
             Ctx.RotationSpeed);
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}