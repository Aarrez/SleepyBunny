using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class CrouchPlayerState : BasePlayerState
    {
        public CrouchPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
        }

        public override void EnterState()
        {
        }

        public override void EnterState(BaseStates.SubStates currentSubState)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(SuperStates currentSuperState)
        {
            throw new System.NotImplementedException();
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