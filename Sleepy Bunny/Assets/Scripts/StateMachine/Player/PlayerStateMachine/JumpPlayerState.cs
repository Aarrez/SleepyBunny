using PlayerStM.BaseStates;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerStM.SuperState
{
    public class JumpPlayerState : BasePlayerState
    {
        public JumpPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        public override void EnterState()
        {
        }

        public override void EnterState(SuperStates currentSuperState)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(BaseStates.SubStates currentSubState)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            if (Ctx.IsClimbing)
            {
                SetSubState(Factory.SuperClimb());
            }
            else if (Ctx.IsFalling && !Ctx.IsClimbing)
            {
                SetSubState(Factory.SubFalling(SuperStates.Jump).Item1);
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}