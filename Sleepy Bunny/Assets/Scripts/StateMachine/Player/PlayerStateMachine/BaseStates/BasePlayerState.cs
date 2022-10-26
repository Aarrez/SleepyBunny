using PlayerStM.SubStates;

namespace PlayerStM.BaseStates
{
    public abstract class BasePlayerState
    {
        protected PlayerStateMachine Ctx;
        protected StateFactory StateFactory;

        public BasePlayerState(PlayerStateMachine ctx, StateFactory stateFactory)
        {
            this.Ctx = ctx;
            this.StateFactory = stateFactory;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
        }

        protected void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();

            Ctx.PlayerState = nextState;
        }

        void SetSuperState(BasePlayerState newSuperState)
        {
        }

        void SetSubState(BasePlayerState newSubState)
        {
        }
    }
}