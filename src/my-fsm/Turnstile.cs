using System;
using Stateless;

namespace my_fsm
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

    public class Turnstile
    {
        StateMachine<State, Trigger> _machine { get; set; }

        public Turnstile()
        {
            this._machine = new StateMachine<State, Trigger>(State.Locked);
            this.SetUp();
        }

        void SetUp()
        {
            this._machine.Configure(State.Locked)
                .Ignore(Trigger.Push)
                .Permit(Trigger.AddCoin, State.Unlocked);

            this._machine.Configure(State.Unlocked)
                .Ignore(Trigger.AddCoin)
                .Permit(Trigger.Push, State.Locked);
        }

        public void AddCoin()
        {
            this._machine.Fire(Trigger.AddCoin);
            Console.WriteLine($"AddCoin -> {this._machine.State}");
        }

        public void Push()
        {
            this._machine.Fire(Trigger.Push);
            Console.WriteLine($"Push -> {this._machine.State}");
        }
    }
}
