using PlayerStM.SubStates;
using UnityEngine;
using System;
using PlayerStM.SuperState;

namespace PlayerStM.BaseStates
{
    /// <summary>
    /// More info of the override methods in
    /// TemplatePlayerState.cs
    /// </summary>
    public abstract class BasePlayerState
    {
        public BasePlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory factory)
        {
            this.Variables = variables;
            this.Methods = methods;
            this.Factory = factory;
            InputScript.Moveing += GetMoveCtx;
            AnimaitonAffected += CheckSwitchAnimation;
        }

        // Varibales for movment
        private Vector3 _moveVector = Vector3.zero;

        private Vector3 _ctxMoveVector;

        private Vector3 _cameraDirection;

        private Vector3 _moveDirection;

        public static Action AnimaitonAffected;

        // Bool used to check if state is Super of sub
        protected bool IsRootState = false;

        // Used to accsess the geters and setters of PlayerStateMachine
        protected PlayerVariables Variables;

        protected PlayerStateMachine Methods;

        // Gain accsess to all the states for switching or other stuff
        protected StateFactory Factory;

        public Vector3 MoveVector => _moveVector;

        public Vector3 MoveDirection => _moveDirection;

        /// <summary>
        /// Idle(BlendTree) = 0, Walk(BlendTree) = 1, <br></br>
        /// Jump = 2, Falling = 3, Land(BlendTree) = 4
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
        /// LandSoft = 0, LandHard = 1 and LandDead = 2
        /// </summary>
        internal enum _eLandAnim : uint
        {
            LandSoft = 0,

            LandHard = 1,

            LandDead = 2
        }

        /// <summary>
        /// Idle = 0, idleClimb = 1,
        /// <br></br>
        /// IdlePull = 2, IdlePush = 3
        /// </summary>
        internal enum _eIdleAnim : uint
        {
            Idle = 0,

            IdleClimb = 1,

            IdlePull = 2,

            IdlePush = 3
        }

        /// <summary>
        /// Walk = 0, Climb = 1,
        /// <br></br>
        /// Pull = 2, Puhs = 3
        /// </summary>
        internal enum _eMoveAnim : uint
        {
            Walk = 0,

            Climb = 1,

            Pull = 2,

            Push = 3,

            Run = 4
        }

        ///<summary>
        /// Called when this state is switched to
        /// </summary>
        public abstract void EnterState();

        ///<summary>
        /// Runs on FixedUpdate()
        /// </summary>
        public abstract void FixedUpdateState();

        ///<summary>
        /// Runs on Update()
        /// </summary>
        public abstract void UpdateState();

        /// <summary>
        /// When switching to another state this method will be called
        /// </summary>
        public abstract void ExitState();

        public abstract void CheckSwitchState();

        /// <summary>
        /// Basicly only used by the inital
        /// super state to have a substate
        /// Do not use if a Sub- or minorstate
        /// </summary>
        public abstract void InitializeSubState();

        public abstract void CheckSwitchAnimation();

        public void FixedUpdateStates()
        {
            Variables.CurrentSuper.FixedUpdateState();
            if (Variables.CurrentSub != null)
            {
                Variables.CurrentSub.FixedUpdateState();
            }
        }

        public void UpdateStates()
        {
            Variables.CurrentSub.UpdateState();
            if (Variables.CurrentSub != null)
            {
                Variables.CurrentSub.UpdateState();
            }
        }

        public void SwitchState(BasePlayerState nextState)
        {
            ExitState();

            nextState.EnterState();

            if (IsRootState)
            {
                Variables.CurrentSuper = nextState;
            }
            else if (Variables.CurrentSub != null)
            {
                Variables.CurrentSuper.SetSubState(nextState);
            }
        }

        protected void SetSuperState(BasePlayerState newSuperState)
        {
            Variables.CurrentSuper = newSuperState;
        }

        protected void SetSubState(BasePlayerState newSubState)
        {
            Variables.CurrentSub = newSubState;
            SetSuperState(this);
        }

        private void GetMoveCtx()
        {
            _ctxMoveVector = Variables.TheInput.MoveCtx.ReadValue<Vector2>();

            switch (Variables.CurrentSuper)
            {
                case SuperClimbingPlayerState:
                    _moveVector
                        = new Vector3(0f, _ctxMoveVector.y, _ctxMoveVector.x);
                    break;

                case SuperGrabPlayerState:
                    _moveVector =
                        new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;

                default:
                    _moveVector =
                       new Vector3(_ctxMoveVector.x, 0f, _ctxMoveVector.y);
                    break;
            }
        }

        internal void MoveCameraDirection()
        {
            _cameraDirection = Variables.MainCamera.transform.TransformDirection(_moveVector);
            _moveDirection = new Vector3(_cameraDirection.x, 0f, _cameraDirection.z);
        }
    }
}