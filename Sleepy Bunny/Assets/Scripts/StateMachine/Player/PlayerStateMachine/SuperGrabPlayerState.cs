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
        public SuperGrabPlayerState(PlayerStateMachine ctx
            , StateFactory factory)
            : base(ctx, factory)
        {
            InitializeSubState();
            IsRootState = true;
        }

        public override void CheckSwitchState()
        {
            if (!Ctx.IsGrabing)
            {
                SwitchState(Factory.SuperGrounded());
            }
        }

        public override void FixedUpdateState()
        {
            CheckSwitchState();

            if (Ctx.IsPulling)
            {
                MovePulledObject(Ctx.transform, Ctx.TransformHit, Ctx.RigidbodyGrabed,
                Ctx.PointHit, Ctx.BreakDistance, Ctx.PullDistance, Ctx.PullForce);
            }
            else if (Ctx.IsPushing)
            {
                MovePushedObject(Ctx.transform, Ctx.TransformHit, Ctx.RigidbodyGrabed,
                    Ctx.PushForce);
            }
        }

        public override void UpdateState()
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            Ctx.TransformHit = null;
            Ctx.RigidbodyGrabed = null;
        }

        public override void InitializeSubState()
        {
            if (Ctx.TheInput.MoveCtx.ReadValueAsButton())
            {
                SetSubState(Factory.SubMovement());
            }
            else
            {
                SetSubState(Factory.SubIdle());
            }
        }
    }
}