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
            else if (Ctx.IsClimbing)
            {
                Debug.Log("climb from jump");
                SwitchState(Factory.SuperClimb());
            }
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.SetInteger("Index",
                (int)_eAnim.Jump);
            AddJumpForce();
            Ctx.IsGrounded = false;
            Debug.Log("Jumping");
        }

        public override void ExitState()
        {
            //Debug.Log(VelocityTest);
            VelocityTest = 0f;
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.SubFalling());
        }

        public override void OnNewSuperState()
        {
        }

        private float VelocityTest = 0f;

        public override void FixedUpdateState()
        {
            if (Ctx.Rb.velocity.y > VelocityTest)
            {
                VelocityTest = Ctx.Rb.velocity.y;
            }
            CheckSwitchState();
        }

        //First half determines jumpheight
        //Second half determines directional distance
        private void AddJumpForce()
        {
            Ctx.Rb.velocity = (Vector3.up * Ctx.JumpHeight)
                + (MoveDirection * Ctx.DirectionalJumpForce);
        }

        public override void UpdateState()
        {
        }
    }
}