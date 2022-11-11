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
        internal BasePlayerState _currentMinorState;

        public BasePlayerState CurrentSuperState
        {
            get => _currentSuperState;
            set => _currentSuperState = value;
        }

        public BasePlayerState CurrentSubState
        {
            get => _currentSubState;
            set => _currentSubState = value;
        }

        public BasePlayerState CurrentMinorState => _currentMinorState;

        internal enum _eGroundAnim : long
        {
            Idle = 0,
            Walking = 1,
            Falling = 2,
            Runing = 3
        }

        internal enum _eJumpAnim : long
        {
            Jump = 0,
            Falling = 1,
            LandSoft = 2,
            LandHard = 3
        }

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
            if (CurrentSubState != null)
            {
                CurrentSubState.UpdateStates();
            }
        }

        public void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();
            Debug.Log(CurrentSuperState);
            if (IsRootState)
            {
                Ctx.PlayerState = nextState;
            }
            else if (CurrentSuperState != null)
            {
                Debug.Log("Setting new substate");
                CurrentSuperState.SetSubState(nextState);
            }
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            CurrentSuperState = newSuperState;
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            CurrentSubState = newSubState;
            SetSuperState(this);
            Debug.Log(this + "set in sub state");
        }

        protected void SetMinorState(BasePlayerState newMinorState)
        {
            _currentMinorState = newMinorState;
            newMinorState.SetSubState(this);
        }
    }
}