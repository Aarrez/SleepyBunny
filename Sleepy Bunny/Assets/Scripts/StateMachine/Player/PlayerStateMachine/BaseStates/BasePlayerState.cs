using PlayerStM.BaseStates;
using PlayerStM.SubStates;
using UnityEngine;
using System;

namespace PlayerStM.BaseStates
{
    public abstract class BasePlayerState
    {
        protected bool IsRootState = false;
        protected PlayerStateMachine Ctx;
        protected StateFactory Factory;
        private BasePlayerState _currentSuperState;
        private BasePlayerState _currentSubState;
        private BasePlayerState _currentMinorState;

        public BasePlayerState CurrnetSuperState =>
            _currentSuperState;

        public BasePlayerState CurrnetSubState =>
            _currentSubState;

        public BasePlayerState CurrentMinorState =>
            _currentMinorState;

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
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public abstract void OnNewSuperState();

        public void UpdateStates()
        {
            UpdateState();
            if (_currentSubState != null)
            {
                _currentSubState.UpdateState();
            }
        }

        protected void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();

            if (IsRootState)
            {
                Ctx.PlayerState = nextState;
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(nextState);
            }
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            _currentSuperState = newSuperState;
            OnNewSuperState();
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.EnterState();
            newSubState.SetSuperState(this);
        }

        protected void SetMinorState(BasePlayerState newMinorState)
        {
            _currentMinorState = newMinorState;
            newMinorState.EnterState();
            newMinorState.SetSubState(this);
        }
    }
}