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

        private RaycastHit _hit;

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
                SwitchState(Factory.SuperJump());
            }
            else if (Ctx.IsGrounded || !Ctx.IsClimbing)
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
            CheckSwitchState();
            OnEdge();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Ctx.Rb.useGravity = true;
            Ctx.IsClimbing = false;
        }

        public override void InitializeSubState()
        {
        }

        private void RotateTwoardsTransform(Transform climbTransform)
        {
            Quaternion rotateTowards = Quaternion.FromToRotation(Ctx.transform.position,
                climbTransform.position);

            Ctx.Rb.rotation = Quaternion.RotateTowards(Ctx.Rb.rotation,
                rotateTowards, Ctx.RotationSpeed);
        }

        private void OnEdge()
        {
            if (!Physics.Raycast(Ctx.transform.position, Ctx.transform.forward,
                 Ctx.ClimbRayLength, Ctx.ClimbLayer))
            {
                Ctx.IsClimbing = false;
                Ctx.IsGrabing = false;
            }
        }
    }
}