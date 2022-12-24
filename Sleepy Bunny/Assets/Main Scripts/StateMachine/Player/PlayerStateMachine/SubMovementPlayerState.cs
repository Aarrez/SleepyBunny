using PlayerStM.BaseStates;
using PlayerStM.SuperState;
using UnityEngine;
using FMODUnity;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// Handles everything with movement so every superstate has this
    /// substate if it involves movment
    /// </summary>
    ///
    public class SubMovementPlayerState : BasePlayerState
    {
        public SubMovementPlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory stateFactory)
            : base(variables, methods, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Variables.TheInput.MoveCtx.ReadValue<Vector2>() == Vector2.zero)
            {
                SwitchState(Factory.SubIdle());
            }
            else if (Variables.Rb.velocity.y < -1f)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            if (Variables.CurrentSuper == Factory.SuperJump()) { return; }

            //switch (Ctx.CurrentSuper)
            //{
            //    case SuperClimbingPlayerState:
            //        Ctx.PlayerAnimator.SetFloat("MoveIndex",
            //            (float)_eMoveAnim.Climb);
            //        break;

            //    case SuperGrabPlayerState:

            //        Ctx.PlayerAnimator.SetFloat("MoveIndex",
            //        (float)_eMoveAnim.Pull);

            //        break;

            //    default:
            //        Ctx.PlayerAnimator.SetFloat("MoveIndex",
            //            (float)_eMoveAnim.Walk);
            //        break;
            //}
            Variables.PlayerAnimator.SetInteger("Index",
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
            switch (Variables.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    PlayerClimb();
                    Variables.PlayerAnimator.SetFloat("MoveIndex",
                       (float)_eMoveAnim.Climb);

                    break;

                case SuperGrabPlayerState:
                    InverseRotateToMovment();
                    PullingMovement();
                    Variables.PlayerAnimator.SetFloat("MoveIndex",
                    (float)_eMoveAnim.Pull);
                    break;

                case SuperJumpPlayerState:
                    if (Variables.AirMovement)
                    {
                        JumpMovemet();
                    }

                    break;

                default:
                    RotateToMovment();
                    if (Variables.TheInput.RunningCtx.ReadValueAsButton())
                    {
                        RunningMovement();
                        Variables.PlayerAnimator.SetFloat("MoveIndex",
                            (float)_eMoveAnim.Run);
                    }
                    else
                    {
                        GroundedMovment();
                        Variables.PlayerAnimator.SetFloat("MoveIndex",
                            (float)_eMoveAnim.Walk);
                    }

                    break;
            }
        }

        private void GroundedMovment()
        {
            Vector3 Movement = MoveDirection * Variables.MovmentForce * Time.fixedDeltaTime;

            Variables.Rb.velocity = new Vector3(
                Movement.x,
                Variables.Rb.velocity.y,
                Movement.z);
        }

        private void RunningMovement()
        {
            Vector3 Movement = MoveDirection *
                Variables.MovmentForce * Variables.RunningModifier * Time.fixedDeltaTime;

            Variables.Rb.velocity = new Vector3(
                Movement.x,
                Variables.Rb.velocity.y,
                Movement.z);
        }

        internal void JumpMovemet()
        {
            Vector3 Movement = MoveDirection *
                (Variables.MovmentForce * Variables.JumpMovementMultipler) * Time.fixedDeltaTime;
            Variables.Rb.velocity = new Vector3(
                Movement.x,
                Variables.Rb.velocity.y,
                Movement.z);
        }

        private void PullingMovement()
        {
            Vector3 Movement = MoveDirection *
                Variables.PushForce * Time.fixedDeltaTime;

            //Ctx.Rb.AddForce(Movement);

            Variables.Rb.velocity = new Vector3(
               Movement.x,
                Variables.Rb.velocity.y,
                Movement.z);
        }

        private void PlayerClimb()
        {
            Variables.Rb.velocity = MoveVector * Variables.ClimbSpeed
                * Time.fixedDeltaTime;
        }

        private void RotateToMovment()
        {
            if (MoveDirection == Vector3.zero) return;

            Variables.Rb.rotation = Quaternion.RotateTowards(Variables.Rb.rotation,
             Quaternion.LookRotation(MoveDirection, Vector3.up),
             Variables.RotationSpeed);
        }

        private void InverseRotateToMovment()
        {
            if (MoveDirection == Vector3.zero) return;

            Variables.Rb.rotation = Quaternion.RotateTowards(Variables.Rb.rotation,
             Quaternion.LookRotation(-MoveDirection, Vector3.up),
             Variables.RotationSpeed);
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}