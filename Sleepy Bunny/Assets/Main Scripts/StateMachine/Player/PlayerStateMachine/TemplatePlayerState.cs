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
        public TemplatePlayerState(
            PlayerVariables variables,
            PlayerStateMachine methods,
            StateFactory stateFactory)
            : base(variables, methods, stateFactory)
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

        public override void InitializeSubState()
        {
        }

        public override void CheckSwitchAnimation()
        {

        }
    }
}