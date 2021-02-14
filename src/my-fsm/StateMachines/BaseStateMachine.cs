using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public abstract class BaseStateMachine<TState, TTrigger>
    {
        internal StateMachine<TState, TTrigger> StateMachine { get; set; }

        public BaseStateMachine(TState initialState)
        {
            this.StateMachine = new StateMachine<TState, TTrigger>(initialState);
            Console.WriteLine($"Initializing to -> {this.StateMachine.State}");
            this.SetUp();
        }

        public abstract void SetUp();
            
        internal void BroadcastStateEntry()
        {
            Console.WriteLine($"  Broadcasting EnteringState -> {this.StateMachine.State}");
        }
        
        internal void BroadcastStateExit()
        {
            Console.WriteLine($"  Broadcasting ExitingState  -> {this.StateMachine.State}");
        }
    }
}
