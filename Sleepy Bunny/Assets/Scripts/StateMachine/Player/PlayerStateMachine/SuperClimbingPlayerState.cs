using System.Numerics;

using PlayerStM.BaseStates;

using UnityEngine.XR.LegacyInputHelpers;

namespace PlayerStM.SubStates
{
    public class SuperClimbingPlayerState : BasePlayerState

    {
        private Vector2 _climbVector;

        public SuperClimbingPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
        }

        public override void CheckSwitchState()
        {
        }

        //If the state is a sub or minor state this method will be called
        public override void EnterState(eStates CurrentState)
        {
            Ctx.Moveing += GetMoveCtx;
        }

        public override void ExitState()
        {
            Ctx.Moveing -= GetMoveCtx;
        }

        public override void InitializeSubState()
        {
            if (_climbVector == Vector2.Zero)
            {
                SwitchState(Factory.SubIdle(eStates.SuperClimb).Item1
                    , Factory.SubIdle(eStates.SuperClimb).Item2);
            }
            else
            {
                SwitchState(Factory.SubMovement(eStates.SuperClimb).Item1
                    , Factory.SubMovement(eStates.SuperClimb).Item2);
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