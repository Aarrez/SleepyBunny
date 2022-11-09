using PlayerStM.BaseStates;

using UnityEngine;

namespace PlayerStM.SuperState
{
    /// <summary>
    /// A superstate that handels the jump function
    /// </summary>
    public class SuperJumpPlayerState : BasePlayerState
    {
        public SuperJumpPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
            //else if (Ctx.IsClimbing)
            //{
            //    SwitchState(Factory.SuperClimb(), eStates.SuperClimb);
            //}
        }

        public override void EnterState()
        {
            IsRootState = true;
            InitializeSubState();
            Debug.Log("jumping");
            AddJumpForce();
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
            else
            {
                SetSubState(Factory.SubFalling());
            }
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        private void AddJumpForce()
        {
            Ctx.Rb.AddForce(Vector3.up * Ctx.JumpHeight, ForceMode.Impulse);
        }
    }
}