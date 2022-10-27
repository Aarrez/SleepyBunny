using PlayerStM.SubStates;
using PlayerStM.SuperState;

using System.Collections.Generic;

using Unity.PlasticSCM.Editor.WebApi;

namespace PlayerStM.BaseStates
{
    public enum States
    {
        Idle,
        Movement,
        Jump,
        Grounded,
        Crouch,
        Climb,
        Falling,
        Pushing,
    }

    public enum SuperStates
    {
        Jump,
        Grounded,
        Climb
    }

    public enum SubStates
    {
        Idle,
        Movement,
        Climb,
        Falling,
        Pushing
    }

    public enum MinorStates
    {
        Crouch
    }

    public class StateFactory
    {
        private PlayerStateMachine _context;
        private (BasePlayerState, SuperStates) some;

        private Dictionary<States, BasePlayerState> _states =
            new Dictionary<States, BasePlayerState>();

        public StateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
            _states[States.Idle] = new IdlePlayerState(_context, this);
            _states[States.Movement] = new MovementPlayerState(_context, this);
            _states[States.Jump] = new JumpPlayerState(_context, this);
            _states[States.Grounded] = new GroundedPlayerState(_context, this);
            _states[States.Crouch] = new CrouchPlayerState(_context, this);
            _states[States.Climb] = new SuperClimbingPlayerState(_context, this);
            _states[States.Falling] = new FallingPlayerState(_context, this);
            _states[States.Pushing] = new PushingPlayerState(_context, this);
        }

        public BasePlayerState SuperJump()
        {
            return _states[States.Jump];
        }

        public BasePlayerState SuperGrounded()
        {
            return _states[States.Grounded];
        }

        public BasePlayerState SuperClimb()
        {
            return _states[States.Climb];
        }

        public (BasePlayerState, SuperStates) SubIdle(SuperStates currentSuperState)
        {
            return (_states[States.Idle], currentSuperState);
        }

        public (BasePlayerState, SuperStates) SubMovement(SuperStates currentSuperState)
        {
            return some = (_states[States.Movement], currentSuperState);
        }

        public (BasePlayerState, SuperStates) SubFalling(SuperStates currentSuperState)
        {
            return (_states[States.Falling], currentSuperState);
        }

        public (BasePlayerState, SuperStates) SubPushing(SuperStates currentSuperState)
        {
            return (_states[States.Pushing], currentSuperState);
        }

        public (BasePlayerState, SubStates) MinorCrouch(SubStates CurrentSubState)
        {
            return (_states[States.Crouch], CurrentSubState);
        }
    }
}