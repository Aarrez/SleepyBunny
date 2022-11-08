using PlayerStM.BaseStates;
using PlayerStM.SuperState;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SubMovementPlayerState : BasePlayerState
    {
        private Vector3 _moveVector;
        private Vector3 _ctxMoveVector;

        public SubMovementPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.MoveCtx.ReadValue<Vector2>() == Vector2.zero)
            {
                SwitchState(Factory.SubIdle());
            }
            else if (!Ctx.IsGrounded)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            Ctx.Moveing += GetMoveCtx;
            Debug.Log("Seeing movemnt");
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveCtx;
            Debug.Log("Not seeing enought movment");
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
            switch (CurrnetSuperState)
            {
                case SuperGroundedPlayerState:
                    _moveVector =
                        new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;

                case SuperJumpPlayerState:
                    _moveVector =
                        new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;

                case SuperClimbingPlayerState:
                    _moveVector
                        = new Vector3(_ctxMoveVector.x, _ctxMoveVector.y, 0f);
                    break;
            }
        }

        private void PlayerMoveing()
        {
            Ctx.Rb.velocity = Ctx.MovementDirection * Ctx.MovmentForce
                * Time.fixedDeltaTime;
        }

        private void RotateToMovment()
        {
            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
                Quaternion.LookRotation(Ctx.MovementDirection, Vector3.up),
                Ctx.RotationSpeed);
        }
    }
}