using System.Numerics;

using PlayerStM.BaseStates;

using UnityEngine.XR.LegacyInputHelpers;

namespace PlayerStM.SubStates
{
    public class SuperClimbingPlayerState : BasePlayerState

    {
        private Vector2 _climbVector;

        public SuperClimbingPlayerState(PlayerStateMachine currentContext, StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
        }

        public override void EnterState()
        {
            Ctx.Moveing += GetMoveCtx;
        }

        public override void EnterState(SuperStates currentSuperState)
        {
            throw new System.NotImplementedException();
        }

        public override void EnterState(BaseStates.SubStates currentSubState)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveCtx;
        }

        public override void InitializeSubState()
        {
            if (_climbVector == Vector2.Zero)
            {
                //SwitchState(Factory.SubIdle());
            }
            else
            {
                //SwitchState(Factory.Movement(States.Climb));
            }
        }

        public override void UpdateState()
        {
            CheckSwitchState();
        }

        private void GetMoveCtx()
        {
            _climbVector = Ctx.MoveCtx.ReadValue<Vector2>();
        }
    }
}