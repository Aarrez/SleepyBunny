using System;
using PlayerStM.BaseStates;
using UnityEngine;

namespace PlayerStM.SubStates
{
    /// <summary>
    /// A super state that tells it's sub state they are pushing something
    /// will handle the push funciton when implememented
    /// </summary>
    public class SuperPushingPlayerState : BasePlayerState
    {
        public SuperPushingPlayerState(PlayerStateMachine ctx
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

            MovePulledObject(Ctx.TransformPulled, Ctx.RigidbodyPulled,
                Ctx.PointHit, Ctx.BreakDistance, Ctx.PullDistance);
        }

        public override void UpdateState()
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            Ctx.TransformPulled = null;
            Ctx.RigidbodyPulled = null;
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