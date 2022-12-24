using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SuperState
{
    /// <summary>
    /// A superstate that handels the jump function
    /// </summary>
    public class SuperJumpPlayerState : BasePlayerState
    {
        public SuperJumpPlayerState(
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
            else if (Variables.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
            else if (Variables.IsClimbing)
            {
                SwitchState(Factory.SuperClimb());
            }
        }

        public override void EnterState()
        {
            Variables.PlayerAnimator.SetInteger("Index",
                (int)_eAnim.Jump);
            Variables.IsGrounded = false;
            Debug.Log("Jumping");
            AddJumpForce();
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
            Methods.GroundedRaycast();
            CheckSwitchState();
        }

        //First half determines jumpheight
        //Second half determines directional distance
        private void AddJumpForce()
        {
            MoveCameraDirection();
            if (!Variables.AirMovement)
            {
                Variables.Rb.velocity = (Vector3.up * Variables.JumpHeight)
                                 + MoveDirection;
            }
            else
            {
                Variables.Rb.velocity = (Vector3.up * Variables.JumpHeight)
                                + (MoveDirection * Variables.DirectionalJumpForce);
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