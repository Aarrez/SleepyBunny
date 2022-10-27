using PlayerStM.BaseStates;

using UnityEngine.InputSystem;

namespace PlayerStM.SuperState
{
    public class GroundedPlayerState : BasePlayerState
    {
        public GroundedPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.JumpCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SuperJump());
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
            if (Ctx.IsFalling)
            {
                SwitchState(Factory.SubFalling(SuperStates.Grounded).Item1
                    , Factory.SubFalling(SuperStates.Grounded).Item2);
            }
            else if (Ctx.MoveCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SubMovement(SuperStates.Grounded).Item1
                    , Factory.SubMovement(SuperStates.Grounded).Item2);
            }
            else
            {
                SwitchState(Factory.SubIdle(SuperStates.Grounded).Item1
                    , Factory.SubIdle(SuperStates.Grounded).Item2);
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}