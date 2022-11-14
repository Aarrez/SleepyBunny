using System;
using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// A super state that tells it's sub state they are pushing something
    /// will handle the push funciton when implememented
    /// </summary>
    public class SuperPushingPlayerState : BasePlayerState
    {
        public SuperPushingPlayerState(PlayerStateMachine ctx
            , StateFactory factory)
            : base(ctx, factory)
        {
            InitializeSubState();
            IsRootState = true;
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

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
        }
    }
}