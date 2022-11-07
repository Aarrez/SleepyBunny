using UnityEngine;

using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    public class SubIdlePlayerState : BasePlayerState
    {
        public SubIdlePlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
            if (Ctx.MoveCtx.ReadValue<Vector2>() != Vector2.zero)
            {
                Debug.Log("Is moveing");
                SwitchState(Factory.SubMovement());
            }
            else if (!Ctx.IsGrounded)
            {
                SwitchState(Factory.SubFalling());
            }
        }

        public override void EnterState()
        {
            Debug.Log("Enterd Idle");
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void OnNewSuperState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
            Debug.Log(this.ToString());
        }
    }
}