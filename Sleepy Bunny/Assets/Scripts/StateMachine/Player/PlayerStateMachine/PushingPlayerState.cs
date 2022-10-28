using System;
using System.Runtime.CompilerServices;

using PlayerStM.BaseStates;

using UnityEditor.Compilation;

using UnityEngine;

namespace PlayerStM.SubStates
{
    public class PushingPlayerState : BasePlayerState
    {
        public PushingPlayerState(PlayerStateMachine ctx
            , StateFactory factory)
            : base(ctx, factory)
        {
        }

        public override void CheckSwitchState()
        {
        }

        public override void EnterState(eStates CurrentState)
        {
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
        }
    }
}