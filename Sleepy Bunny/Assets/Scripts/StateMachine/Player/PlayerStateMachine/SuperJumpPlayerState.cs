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
            AddJumpForce();
            Debug.Log("Jumping");
        }

        public override void ExitState()
        {
            Ctx.PlayerAnimator.ResetTrigger("Jump");
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.SubFalling());
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        //First half determines jumpheight
        //Second half determines directional distance
        private void AddJumpForce()
        {
            Ctx.Rb.velocity = (Vector3.up * Ctx.JumpHeight)
                + (MoveDirection * Ctx.DirectionalJumpForce);
        }
    }
}