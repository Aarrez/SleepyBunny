using PlayerStM.SubStates;

namespace PlayerStM.BaseStates
{
    public abstract class BasePlayerState
    {
        protected PlayerStateMachine Ctx;
        protected StateFactory Factory;
        private BasePlayerState _currentSuperState;
        private BasePlayerState _currentSubState;

        public BasePlayerState CurrnetSuperState =>
            _currentSuperState;

        public BasePlayerState CurrnetSubState =>
            _currentSubState;

        public BasePlayerState(PlayerStateMachine ctx, StateFactory factory)
        {
            this.Ctx = ctx;
            this.Factory = factory;
        }

        public abstract void EnterState();

        public abstract void EnterState(SuperStates currentSuperState);

        public abstract void EnterState(SubStates currentSubState);

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

        protected void SwitchState(BasePlayerState nextState, SuperStates currentSuperState)
        {
            ExitState();

            nextState.EnterState();

            Ctx.PlayerState = nextState;
        }

        protected void SwitchState(BasePlayerState nextState, SubStates currentSubState)
        {
            EnterState();

            nextState.EnterState();

            Ctx.PlayerState = nextState;
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            _currentSubState = newSuperState;
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }
}