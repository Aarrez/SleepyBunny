using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class MovementPlayerState : BasePlayerState
    {
        private Vector3 _moveVector;
        private SuperStates _currentSuperState;

        public MovementPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (_moveVector == Vector3.zero)
            {
                SwitchState(Factory.SubIdle(SuperStates.Grounded).Item1
                    , Factory.SubIdle(SuperStates.Grounded).Item2);
            }
            else if (!Ctx.IsGrounded)
            {
                SwitchState(Factory.SubFalling(SuperStates.Grounded).Item1
                    , Factory.SubFalling(SuperStates.Grounded).Item2);
            }
            else if (Ctx.IsClimbing)
            {
                SwitchState(Factory.SuperClimb());
            }
        }

        public override void EnterState(SuperStates currentSuperState)
        {
            Ctx.Moveing += GetMoveCtx;
        }

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(BaseStates.SubStates currentSubState)
        {
            throw new System.NotImplementedException();
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
            PlayerMoving();
        }

        private void GetMoveCtx()
        {
            Vector2 moveVector = Ctx.MoveCtx.ReadValue<Vector2>();
            _moveVector = new Vector3(moveVector.x, 0f, moveVector.y);
        }

        private void PlayerMoving()
        {
            Ctx.Rb.AddForce(_moveVector * Ctx.Force);
        }
    }
}