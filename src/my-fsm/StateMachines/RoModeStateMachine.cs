using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class RoModeStateMachine
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

        StateMachine<State, Trigger> _StateMachine { get; set; }

        public RoModeStateMachine(State initialState = State.Dispatch)
        {
            this._StateMachine = new StateMachine<State, Trigger>(initialState);
            Console.WriteLine($"Initializing to -> {this._StateMachine.State}");
            this.SetUp();
        }

        void SetUp()
        {
            this._StateMachine.Configure(State.Dispatch)
                .Permit(Trigger.SendToInspection, State.Inspection);
                
            this._StateMachine.Configure(State.Inspection)
                .Permit(Trigger.SendToPendingApproval, State.PendingApproval);
                
            this._StateMachine.Configure(State.PendingApproval)
                .Permit(Trigger.SendToRepair, State.Repair);
                
            this._StateMachine.Configure(State.Repair)
                .Permit(Trigger.SendToReview, State.Review);
                
            this._StateMachine.Configure(State.Review)
                .Permit(Trigger.SendToClosed, State.Closed);
                
            this._StateMachine.Configure(State.Closed)
                .Permit(Trigger.SendToDispatch, State.Dispatch);
        }

        public void GoToNextState()
        {
            switch(this._StateMachine.State)
            {
                case State.Dispatch:
                    this._StateMachine.Fire(Trigger.SendToInspection);
                    break;

                case State.Inspection:
                    this._StateMachine.Fire(Trigger.SendToPendingApproval);
                    break;

                case State.PendingApproval:
                    this._StateMachine.Fire(Trigger.SendToRepair);
                    break;

                case State.Repair:
                    this._StateMachine.Fire(Trigger.SendToReview);
                    break;

                case State.Review:
                    this._StateMachine.Fire(Trigger.SendToClosed);
                    break;

                case State.Closed:
                    this._StateMachine.Fire(Trigger.SendToDispatch);
                    break;
            }

            Console.WriteLine($"RoMode -> {this._StateMachine.State}");
        }
    }
}
