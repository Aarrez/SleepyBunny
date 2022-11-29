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

        private Vector3 _cameraDirection;

        private Vector3 _moveDirection;

        protected RaycastHit Hit;

        // Bool used to check if state is Super of sub
        protected bool IsRootState = false;

        // Used to accsess the geters and setters of PlayerStateMachine
        protected PlayerStateMachine Ctx;

        // Gain accsess to all the states for switching or other stuff
        protected StateFactory Factory;

        public Vector3 MoveDirection => _moveDirection;

        /// <summary>
        /// Idle = 0, Walk = 1, <br></br>
        /// Jump = 2, Falling = 3, Land(Leads to a blendtree) = 4.
        /// </summary>
        internal enum _eAnim : uint
        {
            Idle = 0,

            Walk = 1,

            Jump = 2,

            Falling = 3,

            Land = 4
        }

        /// <summary>
        /// LandSoft = 0, LandHard = 1 and LandDead = 2.
        /// </summary>
        internal enum _eLandAnim : uint
        {
            LandSoft = 0,

            LandHard = 1,

            LandDead = 2
        }

        public BasePlayerState(PlayerStateMachine ctx, StateFactory factory)
        {
            this.Ctx = ctx;
            this.Factory = factory;
            InputScript.Moveing += GetMoveCtx;
            InputScript.Interact += GrabClimb;
        }

        public abstract void EnterState();

        public abstract void FixedUpdateState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public abstract void OnNewSuperState();

        public void FixedUpdateStates()
        {
            Ctx.CurrentSuper.FixedUpdateState();
            if (Ctx.CurrentSub != null)
            {
                Ctx.CurrentSub.FixedUpdateState();
            }
        }

        public void UpdateStates()
        {
            Ctx.CurrentSub.UpdateState();
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
            _ctxMoveVector = Ctx.TheInput.MoveCtx.ReadValue<Vector2>();

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
            _cameraDirection = Ctx.MainCamera.transform.TransformDirection(_moveVector);
            _moveDirection = new Vector3(_cameraDirection.x, 0f, _cameraDirection.z);
        }

        private void GrabClimb()
        {
            Ctx.ClimbGrab(Hit);
        }
    }
}