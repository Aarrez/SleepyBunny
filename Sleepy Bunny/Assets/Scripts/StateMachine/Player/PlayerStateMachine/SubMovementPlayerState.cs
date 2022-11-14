using BulletSharp;

using FMODUnity;

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
            //if (Ctx.CrouchCtx.ReadValueAsButton())
            //    SetSubState(Factory.MinorCrouch());
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

                case SuperJumpPlayerState: break;

                default:
                    GroundedMovment();
                    break;
            }
        }

        private void GroundedMovment()
        {
            Ctx.Rb.velocity = _moveDirection * Ctx.MovmentForce
                * Time.fixedDeltaTime;
        }

        private void RotateToMovment()
        {
            if (_moveDirection == Vector3.zero) return;

            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
                Quaternion.LookRotation(_moveDirection, Vector3.up),
                Ctx.RotationSpeed);
        }
    }
}