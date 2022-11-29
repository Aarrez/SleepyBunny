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
        }

        public override void CheckSwitchState()
        {
            if (Ctx.TheInput.JumpCtx.ReadValueAsButton())
            {
                PushAwayFromClimb();
                SwitchState(Factory.SuperJump());
            }
            else if (Ctx.IsGrounded)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        //If the state is a sub or minor state this method will be called
        public override void EnterState()
        {
            Debug.Log("Climbing");
            Ctx.Rb.useGravity = false;
            Ctx.Rb.velocity = Vector3.zero;
        }

        public override void FixedUpdateState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchState();
            PlayerClimb();
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

        private void PushAwayFromClimb()
        {
            Ctx.Rb.AddExplosionForce(10f, Vector3.forward, 1f);
        }

        // Use in UpdateState()
        private void PlayerClimb()
        {
            Ctx.transform.Translate(Ctx.transform.position + MoveVector, Space.World);
        }
    }
}