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

        public StateMachine<State, Trigger> StateMachine { get; set; }

        public TurnstileStatemachine(State initialState = State.Locked)
        {
            this.StateMachine = new StateMachine<State, Trigger>(initialState);
            Console.WriteLine($"Initializing to -> {this.StateMachine.State}");
            this.SetUp();
        }

        private void SetUp()
        {
            this.StateMachine.Configure(State.Locked)
                .Ignore(Trigger.Push)
                .Permit(Trigger.AddCoin, State.Unlocked)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());

            this.StateMachine.Configure(State.Unlocked)
                .Ignore(Trigger.AddCoin)
                .Permit(Trigger.Push, State.Locked)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }
            
        internal void BroadcastStateEntry()
        {
            Console.WriteLine($"  Broadcasting EnteringState -> {this.StateMachine.State}");
        }
        
        internal void BroadcastStateExit()
        {
            Console.WriteLine($"  Broadcasting ExitingState  -> {this.StateMachine.State}");
        }

        public void AddCoin()
        {
            // Add a coin

            // Then update the state
            this.StateMachine.Fire(Trigger.AddCoin);
        }

        public void Push()
        {
            // Push the gate

            // Then update the state
            this.StateMachine.Fire(Trigger.Push);
        }
    }
}
