using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerStM.BaseStates;

namespace PlayerStm.SubStates
{
}

public class TemplatePlayerState : BasePlayerState
{
    public TemplatePlayerState(PlayerStateMachine ctx,
        StateFactory factory) :
        base(ctx, factory)
    {
    }

    //This should be called in UpdateState to consistenly switch
    //between
    public override void CheckSwitchState()
    {
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }

    //Do not use if a Sub- or minorstate
    public override void InitializeSubState()
    {
    }

    public override void OnNewSuperState()
    {
    }
}