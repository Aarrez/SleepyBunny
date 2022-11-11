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
            IsRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetTrigger("Jump");
            Ctx.PlayerAnimator.SetFloat("LandEffect", (float)_eJumpAnim.Jump);
            Debug.Log("Jumping");
            AddJumpForce();
        }

        public override void ExitState()
        {
            Ctx.PlayerAnimator.ResetTrigger("Jump");
        }

        public override void InitializeSubState()
        {
            //if (Ctx.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            //{
            //    SetSubState(Factory.SubMovement());
            //}
            //else
            //{
            //    SetSubState(Factory.SubFalling());
            //}
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
            Debug.Log("jumpting");
        }
    }
}