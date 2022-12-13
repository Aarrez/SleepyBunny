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
        }

        public override void CheckSwitchState()
        {
            SwitchState(Factory.SuperGrounded());
        }

        public override void EnterState()
        {
            AnimationFunctionManager.Deathdone += CheckSwitchState;
            Ctx.PlayerDied();
        }

        public override void FixedUpdateState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Ctx.PlayerRespawn();
            AnimationFunctionManager.Deathdone -= CheckSwitchState;
        }

        public override void InitializeSubState()
        {
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}