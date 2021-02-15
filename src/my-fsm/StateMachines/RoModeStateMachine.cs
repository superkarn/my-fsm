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
            SendToDispatch,
            SendToInspection,
            SendToPendingApproval,
            SendToRepair,
            SendToReview,
            SendToClosed,
        }

        public RoModeStateMachine() : base(State.Dispatch)
        { }
        
        public RoModeStateMachine(State initialState) : base(initialState)
        { }

        public override void SetUp()
        {
            // TODO complete the state transitions
            this.StateMachine.Configure(State.Dispatch)
                .Permit(Trigger.SendToInspection, State.Inspection)
                .Permit(Trigger.SendToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Inspection)
                .Permit(Trigger.SendToPendingApproval, State.PendingApproval)
                .Permit(Trigger.SendToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.PendingApproval)
                .Permit(Trigger.SendToRepair, State.Repair)
                .Permit(Trigger.SendToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Repair)
                .Permit(Trigger.SendToReview, State.Review)
                .Permit(Trigger.SendToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Review)
                .Permit(Trigger.SendToClosed, State.Closed)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Closed)
                .Permit(Trigger.SendToDispatch, State.Dispatch)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }
    }
}
