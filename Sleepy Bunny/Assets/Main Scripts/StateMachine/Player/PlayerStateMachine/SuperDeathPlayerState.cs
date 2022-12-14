using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    public class SuperDeathPlayerState : BasePlayerState
    {
        /// <summary>
        /// The constructor inherits all the non-local variables form the
        /// base class though geters and setters
        /// </summary>
        public SuperDeathPlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
            IsRootState = true;
            AnimationFunctionManager.Deathdone += AnimaionDone;
        }

        public override void CheckSwitchState()
        {
            if (Ctx.IsDead) { return; }
            SwitchState(Factory.SuperGrounded());
        }

        public override void EnterState()
        {
            InitializeSubState();
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Ctx.PlayerRespawn();
        }

        public override void InitializeSubState()
        {
            SetSubState(Factory.SubIdle());
        }

        public override void CheckSwitchAnimation()
        {
        }

        private void AnimaionDone()
        {
            Debug.Log("Ishappening");
            Ctx.IsDead = false;
        }
    }
}