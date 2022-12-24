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

        public SuperClimbingPlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory stateFactory)
            : base(variables, methods, stateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

        public override void CheckSwitchState()
        {
            if (Variables.TheInput.JumpCtx.ReadValueAsButton())
            {
                SwitchState(Factory.SuperJump());
            }
            else if (!Variables.IsClimbing)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        //If the state is a sub or minor state this method will be called
        public override void EnterState()
        {
            Debug.Log("Climbing");
            Variables.Rb.useGravity = false;
            Variables.Rb.velocity = Vector3.zero;
            Variables.PlayerAnimator.SetFloat("IdleIndex",
                (float)_eIdleAnim.IdleClimb);
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
            Variables.Rb.useGravity = true;
            Variables.IsClimbing = false;
        }

        public override void InitializeSubState()
        {
        }

        private void RotateTwoardsTransform(Transform climbTransform)
        {
            Quaternion rotateTowards = Quaternion.FromToRotation(Methods.transform.position,
                climbTransform.position);

            Variables.Rb.rotation = Quaternion.RotateTowards(Variables.Rb.rotation,
                rotateTowards, Variables.RotationSpeed);
        }

        private void OnEdge()
        {
            for (int i = 0; i < Variables.ForwardVector.Count; i++)
            {
                Debug.DrawRay(Methods.transform.position,
                   Methods.transform.TransformDirection(Variables.ForwardVector[i]),
                    Color.red, 1);
                if (Physics.Raycast(Methods.transform.position,
                    Methods.transform.TransformDirection(Variables.ForwardVector[i]),
                 Variables.ClimbRayLength, Variables.ClimbLayer))
                {
                    return;
                }
                else
                {
                    Variables.IsClimbing = false;
                }
            }
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}