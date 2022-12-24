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
        public SuperDeathPlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory factory) :
            base(variables, methods, factory)
        {
            IsRootState = true;
            AnimationFunctionManager.Deathdone += AnimaionDone;
        }

        public override void CheckSwitchState()
        {
            if (Variables.IsDead) { return; }
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
            Methods.PlayerRespawn();
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
            Variables.IsDead = false;
        }
    }
}