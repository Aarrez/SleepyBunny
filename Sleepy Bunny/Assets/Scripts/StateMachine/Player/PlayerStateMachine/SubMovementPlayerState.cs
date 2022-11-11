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
        private Vector3 _moveVector = Vector3.zero;
        private Vector3 _ctxMoveVector;
        private Vector3 _moveDirection;

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
            Ctx.Moveing += GetMoveCtx;
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveCtx;
        }

        public override void InitializeSubState()
        {
            //if (Ctx.CrouchCtx.ReadValueAsButton())
            //    SetMinorState(Factory.MinorCrouch());
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
            PlayerMoveing();
            RotateToMovment();
        }

        private void GetMoveCtx()
        {
            _ctxMoveVector = Ctx.MoveCtx.ReadValue<Vector2>();
            _moveVector = new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
            Ctx.PlayerAnimator.SetFloat("GSIndex",
                (float)_eGroundAnim.Walking);
            _moveDirection = Ctx.MainCamera.transform.TransformDirection(_moveVector);

            //switch (_currentSuperState)
            //{
            //    case SuperGroundedPlayerState:
            //        _moveVector =
            //            new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
            //        Ctx.PlayerAnimator.SetFloat("GSIndex",
            //            (float)_eGAnim.Walking);

            //        break;

            //    case SuperJumpPlayerState:
            //        _moveVector =
            //            new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);

            //        break;

            //    case SuperClimbingPlayerState:
            //        _moveVector
            //            = new Vector3(_ctxMoveVector.x, _ctxMoveVector.y, 0f);

            //        break;

            //    case SuperPushingPlayerState:
            //        _moveVector =
            //            new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);

            //        break;
            //}
        }

        private void PlayerMoveing()
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