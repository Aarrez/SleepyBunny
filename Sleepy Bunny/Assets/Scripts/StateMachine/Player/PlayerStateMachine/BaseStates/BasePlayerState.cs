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
        // Varibales for movment
        private Vector3 _moveVector = Vector3.zero;

        private Vector3 _ctxMoveVector;

        private Vector3 _cameraDirection;

        private Vector3 _moveDirection;

        // Bool used to check if state is Super of sub
        protected bool IsRootState = false;

        // Used to accsess the geters and setters of PlayerStateMachine
        protected PlayerStateMachine Ctx;

        // Gain accsess to all the states for switching or other stuff
        protected StateFactory Factory;

        public Vector3 MoveVector => _moveVector;

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
        }

        public abstract void EnterState();

        public abstract void FixedUpdateState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

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
                        = new Vector3(0f, _ctxMoveVector.y, _ctxMoveVector.x);
                    break;

                case SuperPushingPlayerState:
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
            _cameraDirection = Ctx.MainCamera.transform.TransformDirection(_moveVector);
            _moveDirection = new Vector3(_cameraDirection.x, 0f, _cameraDirection.z);
        }

        /// <summary>
        /// This medthod pulls an object towards the object with the PlayerStateMachine
        /// script on it.
        ///<br></br>
        /// <param name="transformToPull">The transform of the object being pulled</param>
        /// <br></br>
        /// <param name="rigidbodyOfObject">The rigidbody of the object being pulled</param>
        /// <br></br>
        /// <param name="pullPoint">Where the ray hit the grabable target</param>
        /// <br></br>
        /// <param name="breakDistance">Is X units away then sever connection</param>
        /// <br></br>
        /// <param name="pullDistance">If object is X units away pull on object</param>
        /// </summary>
        internal void MovePulledObject(Transform transformToPull,
            Rigidbody rigidbodyOfObject, Transform pullPoint,
            float breakDistance, float pullDistance)
        {
            float distance = Vector3.Distance(Ctx.transform.position, transformToPull.position);
            if (distance > breakDistance)
            {
                Ctx.IsGrabing = false;
                Debug.Log("Connection Break");
                return;
            }
            if (distance < pullDistance)
            {
                return;
            }

            Vector3 pullDirection = Ctx.transform.position - transformToPull.position;
            Vector3 normalizedDirection = pullDirection.normalized;

            Debug.Log(pullPoint);

            rigidbodyOfObject.AddForceAtPosition
                ((normalizedDirection * Ctx.PullForce * Time.fixedDeltaTime), pullPoint.position);

            //rigidbodyOfObject.velocity += normalizedDirection * Ctx.PullForce * Time.fixedDeltaTime;
        }

        internal void MovePushedObject(Transform transformToPush, Rigidbody rigidbodyOfObject)
        {
        }
    }
}