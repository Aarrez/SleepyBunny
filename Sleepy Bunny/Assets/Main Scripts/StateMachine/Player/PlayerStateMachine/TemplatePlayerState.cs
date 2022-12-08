using PlayerStM.BaseStates;

namespace PlayerStM.SubStates
{
    /*
     * A template for every state.
     * If you need more info check the programming
     * design document in the google drive.
     */

    public class TemplatePlayerState : BasePlayerState
    {
        /// <summary>
        /// The constructor inherits all the non-local variables form the
        /// base class though geters and setters
        /// </summary>
        public TemplatePlayerState(PlayerStateMachine ctx,
            StateFactory factory) :
            base(ctx, factory)
        {
        }

        /// <summary>
        /// This should be called in UpdateState to consistenly
        /// switch between different states
        /// This is a sugestion another way would be to use events
        /// </summary>
        public override void CheckSwitchState()
        {
        }
        
        public override void EnterState()
        {
        }
        
        public override void FixedUpdateState()
        {
            CheckSwitchState();
        }

       
        public override void UpdateState()
        {
        }
        
        public override void ExitState()
        {
        }
        /// <summary>
        /// Basicly only used by the inital super state to have a substate 
        /// Do not use if a Sub- or minorstate
        /// </summary>
        public override void InitializeSubState()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CheckSwitchAnimation()
        {
          
        }
    }
}