using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerStM.BaseStates;
using PlayerStM.SuperState;

namespace PlayerStM.SubStates
{
    public class SubLandPlayerState : BasePlayerState
    {
        //The constructor inherits all the non-local variables form the
        //base class though geters and setters
        public SubLandPlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
        }

        //This should be called in UpdateState to consistenly
        //switch between different states
        public override void CheckSwitchState()
        {
            SwitchState(Factory.SubIdle());
        }

        //Called when this state is switched to
        public override void EnterState()
        {
            AnimationFunctionManager.LandAnimation += CheckSwitchState;

            Ctx.PlayerAnimator.SetFloat("AnimIndex",
            (float)_eAnim.Land);

            Ctx.PlayerAnimator.SetFloat("LandEffect",
                (float)_eLandAnim.LandSoft);
        }

        //Runs on FixedUpdate
        public override void UpdateState()
        {
        }

        //When switching to another state this method will be called
        public override void ExitState()
        {
            AnimationFunctionManager.LandAnimation -= CheckSwitchState;
        }

        //Do not use if a Sub- or minorstate
        public override void InitializeSubState()
        {
        }

        //When a new superstate is set this medtod is called
        public override void OnNewSuperState()
        {
        }
    }
}