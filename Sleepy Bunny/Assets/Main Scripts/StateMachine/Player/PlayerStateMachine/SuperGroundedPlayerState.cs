using System;
using UnityEngine;
using PlayerStM.BaseStates;

namespace PlayerStM.SuperState
{
    /// <summary>
    /// The most default super state becasue it handles everything while on ground
    /// and not doing a more advanced task
    /// </summary>
    public class SuperGroundedPlayerState : BasePlayerState
    {
        public SuperGroundedPlayerState(
            PlayerVariables variables, 
            PlayerStateMachine methods, 
            StateFactory stateFactory)
            : base(variables, methods, stateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Variables.IsDead)
            {
                SwitchState(Factory.SuperDead());
            }
            else if (Variables.TheInput.JumpCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SuperJump());
            }
            else if (Variables.IsClimbing)
            {
                SwitchState(Factory.SuperClimb());
            }
            else if (Variables.IsGrabing)
            {
                SwitchState(Factory.SuperGrab());
            }
        }

        public override void EnterState()
        {
            Debug.Log("Grounded");
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            if (Variables.TheInput.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                SetSubState(Factory.SubMovement());
            }
            else
            {
                SetSubState(Factory.SubIdle());
            }

            //else if (!Ctx.IsGrounded)
            //{
            //    SetSubState(Factory.SubFalling());
            //}
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        public override void UpdateState()
        {
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}