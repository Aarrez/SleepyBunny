using PlayerStM.SubStates;
using PlayerStM.SuperState;

using System.Collections.Generic;

namespace PlayerStM.BaseStates
{
    public enum eStates
    {
        SubIdle,
        SubMovement,
        SuperJump,
        SuperGrounded,
        MinorCrouch,
        SuperClimb,
        SubFalling,
        SuperPushing
    }

    public class StateFactory
    {
        private PlayerStateMachine _context;

        private Dictionary<eStates, BasePlayerState> _states = new();

        public StateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;

            _states[eStates.MinorCrouch] = new MinorCrouchPlayerState(_context, this);
            _states[eStates.SubMovement] = new SubMovementPlayerState(_context, this);
            _states[eStates.SubIdle] = new SubIdlePlayerState(_context, this);
            _states[eStates.SubFalling] = new SubFallingPlayerState(_context, this);
            _states[eStates.SuperPushing] = new SuperPushingPlayerState(_context, this);
            _states[eStates.SuperClimb] = new SuperClimbingPlayerState(_context, this);
            _states[eStates.SuperJump] = new SuperJumpPlayerState(_context, this);
            _states[eStates.SuperGrounded] = new SuperGroundedPlayerState(_context, this);
        }

        public BasePlayerState SuperJump()
        {
            return _states[eStates.SuperJump];
        }

        public BasePlayerState SuperGrounded()
        {
            return _states[eStates.SuperGrounded];
        }

        public BasePlayerState SuperPushing()
        {
            return _states[eStates.SuperPushing];
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

        public BasePlayerState MinorCrouch()
        {
            return _states[eStates.MinorCrouch];
        }
    }
}