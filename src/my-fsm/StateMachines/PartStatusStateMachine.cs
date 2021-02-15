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
            SetToWaiting,
            SetToWorking,
            SetToViewed,
            SetToDefault,
        }

        public PartStatusStateMachine() : base(State.Default)
        { }
        
        public PartStatusStateMachine(State initialState) : base(initialState)
        { }

        public override void SetUp()
        {
            this.StateMachine.Configure(State.Default)
                .Permit(Trigger.SetToWaiting, State.Waiting)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Waiting)
                .Permit(Trigger.SetToWorking, State.Working)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Working)
                .Permit(Trigger.SetToViewed, State.Viewed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Viewed)
                .Permit(Trigger.SetToDefault, State.Default)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }
    }
}
