using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    /*
     * A template for every state.
     * If you need more info check the programming
     * design document in the google drive.
     */

    public class TemplatePlayerState : BasePlayerState
    {
        // The constructor inherits all the non-local variables form the
        // base class though geters and setters
        public TemplatePlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
        }

        // This should be called in UpdateState to consistenly
        // switch between different states
        // This is a sugestion another way would be to use events
        public override void CheckSwitchState()
        {
        }

        // Called when this state is switched to
        public override void EnterState()
        {
        }

        // Runs on FixedUpdate()
        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        // Runs on Update()
        public override void UpdateState()
        {
        }

        // When switching to another state this method will be called
        public override void ExitState()
        {
        }

        // Do not use if a Sub- or minorstate
        public override void InitializeSubState()
        {
        }
    }
}