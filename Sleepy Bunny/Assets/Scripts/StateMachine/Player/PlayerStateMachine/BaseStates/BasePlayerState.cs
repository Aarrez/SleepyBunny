using PlayerStM.BaseStates;
using PlayerStM.SubStates;

using Unity.VisualScripting;

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

        /// <summary>
        /// Only use if the current state has a superior state.
        /// <br></br>
        /// do not use on Superstates it will not work
        /// </summary>
        /// <param name="SuperiorState"></param>
        public abstract void EnterState(eStates SuperiorState);

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
        }

        protected void SwitchState(BasePlayerState nextState, eStates currentSuperiorState)
        {
            ExitState();

            nextState.EnterState(currentSuperiorState);

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