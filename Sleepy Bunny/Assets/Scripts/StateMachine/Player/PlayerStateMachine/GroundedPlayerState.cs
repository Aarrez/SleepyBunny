using PlayerStM.BaseStates;

using UnityEngine.InputSystem;

namespace PlayerStM.SuperState
{
    public class GroundedPlayerState : BasePlayerState
    {
        public GroundedPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.JumpCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SuperJump(), eStates.SuperJump);
            }
        }

        public override void EnterState(eStates CurrentState)
        {
            Ctx.CurrentSuperState = CurrentState;
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            if (Ctx.IsFalling)
            {
                SwitchState(Factory.SubFalling(eStates.SuperGrounded).Item1
                    , Factory.SubFalling(eStates.SuperGrounded).Item2);
            }
            else if (Ctx.MoveCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SubMovement(eStates.SuperGrounded).Item1
                    , Factory.SubMovement(eStates.SuperGrounded).Item2);
            }
            else
            {
                SwitchState(Factory.SubIdle(eStates.SuperGrounded).Item1
                    , Factory.SubIdle(eStates.SuperGrounded).Item2);
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}