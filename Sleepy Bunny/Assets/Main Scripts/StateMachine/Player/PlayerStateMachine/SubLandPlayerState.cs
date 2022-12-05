using PlayerStM.BaseStates;
using System.Threading.Tasks;

namespace PlayerStM.SubStates
{
    public class SubLandPlayerState : BasePlayerState
    {
        //The constructor inherits all the non-local variables form the
        //base class though geters and setters
        public SubLandPlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
        }

        //This should be called in UpdateState to consistenly
        //switch between different states
        public override void CheckSwitchState()
        {
        }

        //Called when this state is switched to
        public override void EnterState()
        {
        }

        //Runs on FixedUpdate
        public override void FixedUpdateState()
        {
        }

        //When switching to another state this method will be called
        public override void ExitState()
        {
        }

        //Do not use if a Sub- or minorstate
        public override void InitializeSubState()
        {
        }

        //When a new superstate is set this medtod is called

        public override void UpdateState()
        {
        }
    }
}