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
            PlayerStateMachine currentContext
            , StateFactory stateFactory
            ) : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (!Ctx.IsGrounded || Ctx.IsClimbing) { return; }

            SwitchState(Factory.SubIdle());
        }

        public override void EnterState()
        {
            Ctx.PlayerAnimator.ResetTrigger("Landed");
            Ctx.PlayerAnimator.SetInteger("Index", (int)_eAnim.Falling);
        }

        public override void FixedUpdateState()
        {
            MinFallVelocity();
            CheckSwitchState();
            Ctx.GroundedRaycast();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            if (Ctx.CurrentSuper != Factory.SuperClimb())
            {
                if (_fallVeloctiy > Ctx.DeadVelocity)
                {
                    Ctx.IsDead = true;
                    Ctx.PlayerDied();
                    return;
                }

                if (_fallVeloctiy > Ctx.SoftHitVelocity)
                {
                    Ctx.PlayerAnimator.SetFloat(
                        "LandEffect", (float)_eLandAnim.LandSoft);
                }
                else if (_fallVeloctiy < Ctx.SoftHitVelocity)
                {
                    Ctx.PlayerAnimator.SetFloat(
                        "LandEffect", (float)_eLandAnim.LandHard);
                }

                Ctx.PlayerAnimator.SetTrigger("Landed");
            }
        }

        public override void InitializeSubState()
        {
        }

        private void MinFallVelocity()
        {
            if (_fallVeloctiy > Ctx.Rb.velocity.y)
            {
                _fallVeloctiy = Ctx.Rb.velocity.y;
            }
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}