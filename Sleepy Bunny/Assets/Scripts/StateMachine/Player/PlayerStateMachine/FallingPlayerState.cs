using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    public class FallingPlayerState : BasePlayerState
    {
        public FallingPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
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