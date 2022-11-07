using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class MinorCrouchPlayerState : BasePlayerState
    {
        public MinorCrouchPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (!Ctx.CrouchCtx.ReadValueAsButton())
            {
                ExitState();
            }
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            SetMinorState(null);
        }

        public override void InitializeSubState()
        {
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}