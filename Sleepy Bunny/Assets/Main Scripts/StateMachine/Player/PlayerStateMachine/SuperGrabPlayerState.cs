using System;
using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// A super state that tells it's sub state they are pushing something
    /// will handle the push funciton when implememented
    /// </summary>
    public class SuperGrabPlayerState : BasePlayerState
    {
        public SuperGrabPlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory factory)
            : base(variables, methods, factory)
        {
            InitializeSubState();
            IsRootState = true;
        }

        public override void CheckSwitchState()
        {
            if (!Variables.IsGrabing)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        public override void EnterState()
        {
            Debug.Log("Grabing");
        }

        public override void FixedUpdateState()
        {
            MovePulledObject(Methods.transform, Variables.TransformHit, Variables.RigidbodyGrabed,
            Variables.PointHit, Variables.BreakDistance, Variables.PullDistance, Variables.PullForce);

            CheckSwitchState();
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Variables.TransformHit = null;
            Variables.RigidbodyGrabed = null;
        }

        public override void InitializeSubState()
        {
            if (Variables.TheInput.MoveCtx.ReadValueAsButton())
            {
                SetSubState(Factory.SubMovement());
            }
            else
            {
                SetSubState(Factory.SubIdle());
            }
        }

        public override void CheckSwitchAnimation()
        {
        }
    }
}