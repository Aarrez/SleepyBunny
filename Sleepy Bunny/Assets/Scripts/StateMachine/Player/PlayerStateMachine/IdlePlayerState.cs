using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class IdlePlayerState : BasePlayerState
    {
        public IdlePlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.CurrentSuperState == eStates.SuperPushing)
            {
                if (Ctx.MoveCtx.ReadValueAsButton())
                {
                    SwitchState(Factory.SubMovement(Ctx.CurrentSuperState).Item1
                        , Ctx.CurrentSuperState);
                }
            }
            else
            {
                if (Ctx.MoveCtx.ReadValueAsButton())
                {
                    SwitchState(Factory.SubMovement(Ctx.CurrentSuperState).Item1
                        , Ctx.CurrentSuperState);
                }
                else if (!Ctx.IsGrounded)
                {
                    SwitchState(Factory.SubFalling(Ctx.CurrentSuperState).Item1
                        , Ctx.CurrentSuperState);
                }
            }
        }

        public override void EnterState(eStates CurrentState)
        {
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}