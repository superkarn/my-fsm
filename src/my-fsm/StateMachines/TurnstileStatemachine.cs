using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class TurnstileStatemachine
    {
        public enum State
        {
            Locked,
            Unlocked
        }

        public enum Trigger
        {
            AddCoin,
            Push
        }

        StateMachine<State, Trigger> _StateMachine { get; set; }

        public TurnstileStatemachine(State initialState = State.Locked)
        {
            this._StateMachine = new StateMachine<State, Trigger>(initialState);
            Console.WriteLine($"Initializing to -> {this._StateMachine.State}");
            this.SetUp();
        }

        void SetUp()
        {
            this._StateMachine.Configure(State.Locked)
                .Ignore(Trigger.Push)
                .Permit(Trigger.AddCoin, State.Unlocked);

            this._StateMachine.Configure(State.Unlocked)
                .Ignore(Trigger.AddCoin)
                .Permit(Trigger.Push, State.Locked);
        }

        public void AddCoin()
        {
            this._StateMachine.Fire(Trigger.AddCoin);
            Console.WriteLine($"Turnstile -> {this._StateMachine.State}");
        }

        public void Push()
        {
            this._StateMachine.Fire(Trigger.Push);
            Console.WriteLine($"Turnstile -> {this._StateMachine.State}");
        }
    }
}
