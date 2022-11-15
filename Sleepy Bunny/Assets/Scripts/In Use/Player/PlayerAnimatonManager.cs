using PlayerStM.BaseStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatonManager : PlayerStateMachine
{
    public void SwitchFromFallingState()
    {
        LandAnimationDone = true;
    }
}