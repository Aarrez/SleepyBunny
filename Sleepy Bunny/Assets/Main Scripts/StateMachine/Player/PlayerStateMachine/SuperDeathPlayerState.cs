using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SuperDeathPlayerState : BasePlayerState
    {
        /// <summary>
        /// The constructor inherits all the non-local variables form the
        /// base class though geters and setters
        /// </summary>
        public SuperDeathPlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
        }

        /// <summary>
        /// This should be called in UpdateState to consistenly
        /// switch between different states
        /// This is a sugestion another way would be to use events
        /// </summary>
        public override void CheckSwitchState()
        {
        }

        public override void EnterState()
        {
        }

        public override void FixedUpdateState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}