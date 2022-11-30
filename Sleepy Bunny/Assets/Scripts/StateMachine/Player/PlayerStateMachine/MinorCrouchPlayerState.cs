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
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        public override void UpdateState()
        {
        }
    }
}