using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class CrouchPlayerState : BasePlayerState
    {
        public CrouchPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
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