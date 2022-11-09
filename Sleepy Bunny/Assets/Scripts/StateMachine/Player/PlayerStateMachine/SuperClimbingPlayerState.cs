using System.Numerics;

using PlayerStM.BaseStates;

using UnityEngine.XR.LegacyInputHelpers;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// A SuperState that tells it substates that it is climbing up a wall
    /// or other objects
    /// </summary>
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
        public override void EnterState()
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
                SwitchState(Factory.SubIdle());
            }
            else
            {
                SwitchState(Factory.SubMovement());
            }
        }

        public override void OnNewSuperState()
        {
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