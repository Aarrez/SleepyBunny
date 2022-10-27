using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class PushingPlayerState : BasePlayerState
    {
        public PushingPlayerState(PlayerStateMachine ctx, StateFactory factory)
            : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public override void InitializeSubState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }
}