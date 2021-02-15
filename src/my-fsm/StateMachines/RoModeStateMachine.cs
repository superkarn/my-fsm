using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class RoModeStateMachine : BaseStateMachine<RoModeStateMachine.State, RoModeStateMachine.Trigger>
    {
        public enum State
        {
            Dispatch,
            Inspection,
            PendingApproval,
            Repair,
            Review,
            Closed
        }

        public enum Trigger
        {
            SetToDispatch,
            SetToInspection,
            SetToPendingApproval,
            SetToRepair,
            SetToReview,
            SetToClosed,
        }

        public RoModeStateMachine() : base(State.Dispatch)
        { }
        
        public RoModeStateMachine(State initialState) : base(initialState)
        { }

        public override void SetUp()
        {
            // TODO complete the state transitions
            this.StateMachine.Configure(State.Dispatch)
                .Permit(Trigger.SetToInspection, State.Inspection)
                .Permit(Trigger.SetToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Inspection)
                .Permit(Trigger.SetToPendingApproval, State.PendingApproval)
                .Permit(Trigger.SetToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.PendingApproval)
                .Permit(Trigger.SetToRepair, State.Repair)
                .Permit(Trigger.SetToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Repair)
                .Permit(Trigger.SetToReview, State.Review)
                .Permit(Trigger.SetToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Review)
                .Permit(Trigger.SetToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Closed)
                .Permit(Trigger.SetToDispatch, State.Dispatch)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }
    }
}
