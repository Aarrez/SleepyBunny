using PlayerStM.SubStates;
using UnityEngine;
using System;
using PlayerStM.SuperState;

namespace PlayerStM.BaseStates
{
    public abstract class BasePlayerState
    {
        // Varibales for movment
        private Vector3 _moveVector = Vector3.zero;

        private Vector3 _ctxMoveVector;

        private Vector3 _moveDirection;

        protected RaycastHit Hit;

        // Bool used to check if state is Super of sub
        protected bool IsRootState = false;

        // Used to accsess the geters and setters of PlayerStateMachine
        protected PlayerStateMachine Ctx;

        // Gain accsess to all the states for switching or other stuff
        protected StateFactory Factory;

        internal enum _eGroundAnim : long
        {
            Idle = 0,

            Walking = 1,

            Falling = 2,

            Land = 3
        }

        internal enum _eJumpAnim : long
        {
            Jump = 0,

            Falling = 1,

            Land = 2
        }

        internal enum _eLandAnim : long
        {
            LandSoft = 0,

            LandHard = 1,

            LandDead = 2
        }

        public Vector3 MoveDirection => _moveDirection;

        public BasePlayerState(PlayerStateMachine ctx, StateFactory factory)
        {
            this.Ctx = ctx;
            this.Factory = factory;
            Ctx.Moveing += GetMoveCtx;
            ctx.Grab += GrabClimb;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public abstract void OnNewSuperState();

        public void UpdateStates()
        {
            Ctx.CurrentSuper.UpdateState();
            if (Ctx.CurrentSub != null)
            {
                Ctx.CurrentSub.UpdateState();
            }
        }

        public void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();

            if (IsRootState)
            {
                Ctx.CurrentSuper = nextState;
            }
            else if (Ctx.CurrentSub != null)
            {
                Ctx.CurrentSuper.SetSubState(nextState);
            }
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            Ctx.CurrentSuper = newSuperState;
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            Ctx.CurrentSub = newSubState;
            SetSuperState(this);
        }

        private void GetMoveCtx()
        {
            _ctxMoveVector = Ctx.MoveCtx.ReadValue<Vector2>();

            switch (Ctx.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    _moveVector
                        = new Vector3(_ctxMoveVector.x, _ctxMoveVector.y, 0f);
                    break;

                case SuperPushingPlayerState:
                    _moveVector =
                        new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;

                case SuperJumpPlayerState:

                    goto default;

                default:
                    _moveVector =
                       new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;
            }
            Vector3 tempV3 = Ctx.MainCamera.transform.TransformDirection(_moveVector);
            _moveDirection = new Vector3(tempV3.x, 0f, tempV3.z);
        }

        private void GrabClimb()
        {
            Ctx.ClimbGrab(Hit);
        }
    }
}