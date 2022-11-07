using System;
using UnityEngine;
using PlayerStM.BaseStates;
using PlayerStM.SubStates;

namespace PlayerStM.SuperState
{
    public class SuperGroundedPlayerState : BasePlayerState
    {
        public SuperGroundedPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.JumpCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SuperJump());
            }
        }

        public override void EnterState()
        {
            IsRootState = true;
            Debug.Log("Enterd grounded");
            InitializeSubState();
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            if (Ctx.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SetSubState(Factory.SubMovement());
            }
            else if (!Ctx.IsGrounded)
            {
                SetSubState(Factory.SubFalling());
            }
            else
            {
                SetSubState(Factory.SubIdle());
            }
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }
    }
}