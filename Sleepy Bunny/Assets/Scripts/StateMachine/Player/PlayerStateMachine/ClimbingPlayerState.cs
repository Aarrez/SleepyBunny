using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class ClimbingPlayerState : BasePlayerState

    {
        public ClimbingPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
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

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}