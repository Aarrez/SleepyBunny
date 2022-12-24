using PlayerStM.SubStates;

using PlayerStM.SuperState;

using System.Collections.Generic;

namespace PlayerStM.BaseStates
{
    public enum eStates
    {
        SubLand,

        SubIdle,

        SubMovement,

        SuperJump,

        SuperGrounded,

        SuperClimb,

        SubFalling,

        SuperGrab,

        SuperDead
    }

    public class StateFactory
    {
        private PlayerStateMachine _methods;
        private PlayerVariables _variables;

        private Dictionary<eStates, BasePlayerState> _states = new();

        public StateFactory(PlayerStateMachine currentMethods, PlayerVariables currentVariables)
        {
            _methods = currentMethods;
            _variables = currentVariables;

            _states[eStates.SubLand] = new SubLandPlayerState(_variables, _methods, this);
            _states[eStates.SubMovement] = new SubMovementPlayerState(_variables, _methods, this);
            _states[eStates.SubIdle] = new SubIdlePlayerState(_variables, _methods, this);
            _states[eStates.SubFalling] = new SubFallingPlayerState(_variables, _methods, this);
            _states[eStates.SuperGrab] = new SuperGrabPlayerState(_variables, _methods, this);
            _states[eStates.SuperClimb] = new SuperClimbingPlayerState(_variables, _methods, this);
            _states[eStates.SuperJump] = new SuperJumpPlayerState(_variables, _methods, this);
            _states[eStates.SuperGrounded] = new SuperGroundedPlayerState(_variables, _methods, this);
            _states[eStates.SuperDead] = new SuperDeathPlayerState(_variables, _methods, this);
        }

        public BasePlayerState SuperDead()
        {
            return _states[eStates.SuperDead];
        }

        public BasePlayerState SuperJump()
        {
            return _states[eStates.SuperJump];
        }

        public BasePlayerState SuperGrounded()
        {
            return _states[eStates.SuperGrounded];
        }

        public BasePlayerState SuperGrab()
        {
            return _states[eStates.SuperGrab];
        }

        public BasePlayerState SuperClimb()
        {
            return _states[eStates.SuperClimb];
        }

        public BasePlayerState SubIdle()
        {
            return _states[eStates.SubIdle];
        }

        public BasePlayerState SubMovement()
        {
            return _states[eStates.SubMovement];
        }

        public BasePlayerState SubFalling()
        {
            return _states[eStates.SubFalling];
        }

        public BasePlayerState SubLand()
        {
            return _states[eStates.SubLand];
        }
    }
}