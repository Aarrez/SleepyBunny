using System.IO;

using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class MovementPlayerState : BasePlayerState
    {
        private Vector3 _moveVector;
        private Vector3 CtxMoveVector;

        public MovementPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (_moveVector == Vector3.zero)
            {
                SwitchState(Factory.SubIdle(eStates.SuperGrounded).Item1
                    , Factory.SubIdle(eStates.SuperGrounded).Item2);
            }
            else if (!Ctx.IsGrounded)
            {
                SwitchState(Factory.SubFalling(eStates.SuperGrounded).Item1
                    , Factory.SubFalling(eStates.SuperGrounded).Item2);
            }
            else if (Ctx.IsClimbing)
            {
                SwitchState(Factory.SuperClimb(), eStates.SuperClimb);
            }
        }

        public override void EnterState(eStates SuperiorState)
        {
            Ctx.Moveing += GetMoveCtx;
            switch (SuperiorState)
            {
                case eStates.SuperGrounded:
                    break;

                case eStates.SuperJump:
                    break;

                case eStates.SuperClimb:
                    break;
            }

            if (SuperiorState == eStates.SuperGrounded)
                _moveVector = new Vector3(CtxMoveVector.x, 0f, CtxMoveVector.y);
            else if (SuperiorState == eStates.SuperJump)
                _moveVector = new Vector3(CtxMoveVector.x, 0f, CtxMoveVector.y);
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveCtx;
        }

        public override void InitializeSubState()
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
        }

        private void PlayerMoveing()
        {
            Ctx.Rb.AddForce(_moveVector * Ctx.Force, ForceMode.Force);
        }
    }
}