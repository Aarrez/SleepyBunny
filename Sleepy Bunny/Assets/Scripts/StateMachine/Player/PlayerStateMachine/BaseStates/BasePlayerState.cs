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
        internal BasePlayerState _currentMinorState;

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

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public abstract void OnNewSuperState();

        public void UpdateStates()
        {
            UpdateState();
            if (Ctx.CurrentSub != null)
            {
                Ctx.CurrentSub.UpdateState();
            }
        }

        protected void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();
            Debug.Log(Ctx.CurrentSuper);
            if (IsRootState)
            {
                Ctx.CurrentSuper = nextState;
            }
            else if (Ctx.CurrentSub != null)
            {
                Debug.Log("Setting new substate");
                Ctx.CurrentSuper.SetSubState(nextState);
            }
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            Ctx.CurrentSuper = newSuperState;
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            Ctx.CurrentSub = newSubState;
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