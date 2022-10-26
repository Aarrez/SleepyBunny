using PlayerStM.SubStates;

namespace PlayerStM.BaseStates
{
    public abstract class BasePlayerState
    {
        protected PlayerStateMachine Ctx;
        protected StateFactory Factory;

        public BasePlayerState(PlayerStateMachine ctx, StateFactory factory)
        {
            this.Ctx = ctx;
            this.Factory = factory;
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

        protected void SetSuperState(BasePlayerState newSuperState)
        {
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
        }
    }
}