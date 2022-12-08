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
            AnimationFunctionManager.AnimationJumpEvent += AddJumpForce;
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
            else if (Ctx.IsClimbing)
            {
                SwitchState(Factory.SuperClimb());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetInteger("Index",
                (int)_eAnim.Jump);
            Ctx.IsGrounded = false;
            Debug.Log("Jumping");
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.SubFalling());
        }

        public override void FixedUpdateState()
        {
            Ctx.GroundedRaycast();
            CheckSwitchState();
        }

        //First half determines jumpheight
        //Second half determines directional distance
        private void AddJumpForce()
        {
            MoveCameraDirection();
            if (!Ctx.AirMovement)
            {
                Ctx.Rb.velocity = (Vector3.up * Ctx.JumpHeight)
                                 + MoveDirection;
            }
            else
            {
                Ctx.Rb.velocity = (Vector3.up * Ctx.JumpHeight)
                                + (MoveDirection * Ctx.DirectionalJumpForce);
            }
        }

        public override void UpdateState()
        {
        }

        public override void CheckSwitchAnimation()
        {
           
        }
    }
}