using System;
using System.Runtime.CompilerServices;

using PlayerStM.BaseStates;

using UnityEditor.Compilation;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SuperPushingPlayerState : BasePlayerState
    {
        public SuperPushingPlayerState(PlayerStateMachine ctx
            , StateFactory factory)
            : base(ctx, factory)
        {
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
            throw new NotImplementedException();
        }

        public override void UpdateState()
        {
        }
    }
}