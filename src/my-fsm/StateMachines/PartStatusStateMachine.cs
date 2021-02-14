using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class PartStatusStateMachine
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

        StateMachine<State, Trigger> _StateMachine { get; set; }

        public PartStatusStateMachine(State initialState = State.Default)
        {
            this._StateMachine = new StateMachine<State, Trigger>(initialState);
            Console.WriteLine($"Initializing to -> {this._StateMachine.State}");
            this.SetUp();
        }

        void SetUp()
        {
            this._StateMachine.Configure(State.Default)
                .Permit(Trigger.SendToWaiting, State.Waiting);
                
            this._StateMachine.Configure(State.Waiting)
                .Permit(Trigger.SendToWorking, State.Working);
                
            this._StateMachine.Configure(State.Working)
                .Permit(Trigger.SendToViewed, State.Viewed);
                
            this._StateMachine.Configure(State.Viewed)
                .Permit(Trigger.SendToDefault, State.Default);
        }

        public void GoToNextState()
        {
            switch(this._StateMachine.State)
            {
                case State.Default:
                    this._StateMachine.Fire(Trigger.SendToWaiting);
                    break;

                case State.Waiting:
                    this._StateMachine.Fire(Trigger.SendToWorking);
                    break;

                case State.Working:
                    this._StateMachine.Fire(Trigger.SendToViewed);
                    break;

                case State.Viewed:
                    this._StateMachine.Fire(Trigger.SendToDefault);
                    break;
            }
            
            Console.WriteLine($"PartStatus -> {this._StateMachine.State}");
        }
    }
}
