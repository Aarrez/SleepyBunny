using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// A SuperState that tells it substates that it is climbing up a wall
    /// or other objects
    /// </summary>
    public class SuperClimbingPlayerState : BasePlayerState
    {
        private RaycastHit Climbhit;

        private Vector2 _climbVector;

        private float _originalMass;

        public SuperClimbingPlayerState(PlayerStateMachine currentContext
            , StateFactory stateFactory)
            : base(currentContext, stateFactory)
        {
            IsRootState = true;
            InitializeSubState();
            Climbhit = base.Hit;
        }

        public override void CheckSwitchState()
        {
            if (Ctx.TheInput.JumpCtx.ReadValueAsButton())
            {
                PushAwayFromClimb();
                SwitchState(Factory.SuperJump());
            }
        }

        //If the state is a sub or minor state this method will be called
        public override void EnterState()
        {
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

        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

        private void PushAwayFromClimb()
        {
            Ctx.Rb.AddExplosionForce(10f, Vector3.forward, 1f);
        }

        public override void UpdateState()
        {
        }
    }
}