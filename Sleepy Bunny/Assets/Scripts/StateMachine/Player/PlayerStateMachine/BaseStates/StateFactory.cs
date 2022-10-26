using PlayerStM.SubStates;
using PlayerStM.SuperState;
using System.Collections.Generic;

namespace PlayerStM.BaseStates
{
    enum States
    {
        idle,
        Movement,
        Jump,
        Grounded,
        Crouch,
        Climb,
        Falling,
    }

    public class StateFactory
    {
        private PlayerStateMachine _context;

        private Dictionary<States, BasePlayerState> _states =
            new Dictionary<States, BasePlayerState>();

        public StateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
            _states[States.idle] = new IdlePlayerState(_context, this);
            _states[States.Movement] = new MovementPlayerState(_context, this);
            _states[States.Jump] = new JumpPlayerState(_context, this);
            _states[States.Grounded] = new GroundedPlayerState(_context, this);
            _states[States.Crouch] = new CrouchPlayerState(_context, this);
            _states[States.Climb] = new ClimbingPlayerState(_context, this);
            _states[States.Falling] = new FallingPlayerState(_context, this);
        }

        public BasePlayerState Idle()
        {
            return _states[States.idle];
        }

        public BasePlayerState Movement()
        {
            return _states[States.Movement];
        }

        public BasePlayerState Jump()
        {
            return _states[States.Jump];
        }

        public BasePlayerState Grounded()
        {
            return _states[States.Grounded];
        }

        public BasePlayerState Crouch()
        {
            return _states[States.Crouch];
        }

        public BasePlayerState Climb()
        {
            return _states[States.Climb];
        }

        public BasePlayerState Falling()
        {
            return _states[States.Falling];
        }
    }
}