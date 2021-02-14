using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class PartStatusStateMachine : BaseStateMachine<PartStatusStateMachine.State, PartStatusStateMachine.Trigger>
    {
        public enum State
        {
            Default,
            Waiting,
            Working,
            Viewed,
        }

        public enum Trigger
        {
            SendToWaiting,
            SendToWorking,
            SendToViewed,
            SendToDefault,
        }

        public PartStatusStateMachine() 
            : base(State.Default)
        { }
        
        public PartStatusStateMachine(State initialState) 
            : base(initialState)
        { }

        public override void SetUp()
        {
            this.StateMachine.Configure(State.Default)
                .Permit(Trigger.SendToWaiting, State.Waiting)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Waiting)
                .Permit(Trigger.SendToWorking, State.Working)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Working)
                .Permit(Trigger.SendToViewed, State.Viewed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Viewed)
                .Permit(Trigger.SendToDefault, State.Default)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }

        public void GoToNextState()
        {
            switch(this.StateMachine.State)
            {
                case State.Default:
                    this.StateMachine.Fire(Trigger.SendToWaiting);
                    break;

                case State.Waiting:
                    this.StateMachine.Fire(Trigger.SendToWorking);
                    break;

                case State.Working:
                    this.StateMachine.Fire(Trigger.SendToViewed);
                    break;

                case State.Viewed:
                    this.StateMachine.Fire(Trigger.SendToDefault);
                    break;
            }
        }
    }
}
