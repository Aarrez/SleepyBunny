using PlayerStM.SubStates;
using PlayerStM.SuperState;

using System.Collections.Generic;

using Unity.PlasticSCM.Editor.WebApi;

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
        SuperPushing,
    }

    public class StateFactory
    {
        private PlayerStateMachine _context;
        private (BasePlayerState, eStates) some;

        private Dictionary<eStates, BasePlayerState> _states =
            new Dictionary<eStates, BasePlayerState>();

        public StateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
            _states[eStates.SubIdle] = new IdlePlayerState(_context, this);
            _states[eStates.SubMovement] = new MovementPlayerState(_context, this);
            _states[eStates.SuperJump] = new JumpPlayerState(_context, this);
            _states[eStates.SuperGrounded] = new GroundedPlayerState(_context, this);
            _states[eStates.MinorCrouch] = new CrouchPlayerState(_context, this);
            _states[eStates.SuperClimb] = new SuperClimbingPlayerState(_context, this);
            _states[eStates.SubFalling] = new FallingPlayerState(_context, this);
            _states[eStates.SuperPushing] = new PushingPlayerState(_context, this);
        }

        public BasePlayerState SuperJump()
        {
            return _states[eStates.SuperJump];
        }

        public BasePlayerState SuperGrounded()
        {
            return _states[eStates.SuperGrounded];
        }

        public BasePlayerState SuperClimb()
        {
            return _states[eStates.SuperClimb];
        }

        public (BasePlayerState, eStates) SubIdle(eStates currentSuperState)
        {
            return (_states[eStates.SubIdle], currentSuperState);
        }

        public (BasePlayerState, eStates) SubMovement(eStates currentSuperState)
        {
            return some = (_states[eStates.SubMovement], currentSuperState);
        }

        public (BasePlayerState, eStates) SubFalling(eStates currentSuperState)
        {
            return (_states[eStates.SubFalling], currentSuperState);
        }

        public (BasePlayerState, eStates) SubPushing(eStates currentSuperState)
        {
            return (_states[eStates.SuperPushing], currentSuperState);
        }

        public (BasePlayerState, eStates) MinorCrouch(eStates CurrentSubState)
        {
            return (_states[eStates.MinorCrouch], CurrentSubState);
        }
    }
}