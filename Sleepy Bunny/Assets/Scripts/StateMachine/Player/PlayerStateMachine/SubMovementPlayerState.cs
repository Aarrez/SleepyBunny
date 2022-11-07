using PlayerStM.BaseStates;
using PlayerStM.SuperState;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SubMovementPlayerState : BasePlayerState
    {
        private Vector3 _moveVector;
        private Vector3 CtxMoveVector;

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
        }

        private void GetMoveCtx()
        {
            CtxMoveVector = Ctx.MoveCtx.ReadValue<Vector2>();
            switch (CurrnetSuperState)
            {
                case SuperGroundedPlayerState:
                    _moveVector = new Vector3(CtxMoveVector.x, 0f, CtxMoveVector.y);
                    break;

                case SuperJumpPlayerState:
                    _moveVector = new Vector3(CtxMoveVector.x, 0f, CtxMoveVector.y);
                    break;

                case SuperClimbingPlayerState:
                    _moveVector = new Vector3(CtxMoveVector.x, CtxMoveVector.y, 0f);
                    break;
            }
        }

        private void PlayerMoveing()
        {
            Ctx.Rb.velocity = _moveVector * Ctx.MovmentForce * Time.fixedDeltaTime;
        }
    }
}