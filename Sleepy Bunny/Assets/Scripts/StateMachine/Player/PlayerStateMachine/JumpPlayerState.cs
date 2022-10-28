using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SuperState
{
    public class JumpPlayerState : BasePlayerState
    {
        public JumpPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded(), eStates.SuperGrounded);
            }
            else if (Ctx.IsClimbing)
            {
                SwitchState(Factory.SuperClimb(), eStates.SuperClimb);
            }
        }

        public override void EnterState(eStates CurrentState)
        {
            Ctx.CurrentSuperState = CurrentState;
            AddJumpForce();
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            if (Ctx.IsFalling)
            {
                SetSubState(Factory.SubFalling(eStates.SuperJump).Item1);
            }
            else if (Ctx.MoveCtx.ReadValue<Vector2>() == Vector2.zero)
            {
                SetSubState(Factory.SubMovement(eStates.SuperJump).Item1);
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        private void AddJumpForce()
        {
            Ctx.Rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}