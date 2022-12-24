using UnityEngine;
using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// This state handles all functionaly when the player is falling
    /// </summary>
    /// If your want more info then please refer to TemplatePlayerState.cs

    public class SubFallingPlayerState : BasePlayerState
    {
        private float _fallVeloctiy = 0f;

        public SubFallingPlayerState
            (
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory factory
            ) : base(variables, methods, factory)
        {
        }

        public override void CheckSwitchState()
        {
            if (!Variables.IsGrounded || Variables.IsClimbing) { return; }

            SwitchState(Factory.SubIdle());
        }

        public override void EnterState()
        {
            Variables.PlayerAnimator.ResetTrigger("Landed");
            Variables.PlayerAnimator.SetInteger("Index", (int)_eAnim.Falling);
        }

        public override void FixedUpdateState()
        {
            MinFallVelocity();
            CheckSwitchState();
            Methods.GroundedRaycast();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            if (Variables.CurrentSuper != Factory.SuperClimb())
            {
                if (_fallVeloctiy < Variables.DeadVelocity)
                {
                    Variables.IsDead = true;
                    Methods.PlayerDied();
                    _fallVeloctiy = 0;
                    return;
                }
                else if (_fallVeloctiy > Variables.SoftHitVelocity)
                {
                    Variables.PlayerAnimator.SetFloat(
                        "LandEffect", (float)_eLandAnim.LandSoft);
                }
                else if (_fallVeloctiy < Variables.SoftHitVelocity)
                {
                    Variables.PlayerAnimator.SetFloat(
                        "LandEffect", (float)_eLandAnim.LandHard);
                }

                Variables.PlayerAnimator.SetTrigger("Landed");
            }
            _fallVeloctiy = 0;
        }

        public override void InitializeSubState()
        {
        }

        private void MinFallVelocity()
        {
            if (_fallVeloctiy > Variables.Rb.velocity.y)
            {
                _fallVeloctiy = Variables.Rb.velocity.y;
            }
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}