public class StateFactory
{
    private PlayerStateMachine _context;

    public StateFactory(PlayerStateMachine currentContext) => _context = currentContext;

    public BasePlayerState Idle()
    {
        return new IdlePlayerState(_context, this);
    }

    public BasePlayerState Walking()
    {
        return new WalkingPlayerState(_context, this);
    }

    public BasePlayerState Jump()
    {
        return new JumpPlayerState(_context, this);
    }

    public BasePlayerState Grounded()
    {
        return new GroundedPlayerState(_context, this);
    }

    public BasePlayerState Crouch()
    {
        return new CrouchPlayerState(_context, this);
    }

    public BasePlayerState Climb()
    {
        return new ClimbingPlayerState(_context, this);
    }
}