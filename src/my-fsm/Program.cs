using System;
using Stateless;

namespace my_fsm
{
    public class Program
    {

        static void Main(string[] args)
        {
            var sm = new StateMachine<State, Trigger>(State.Locked);
            SetUp(sm);
    
            Console.WriteLine($"{sm.State}");

            Console.Write($"AddCoin -> ");
            sm.Fire(Trigger.AddCoin);
            Console.WriteLine($"{sm.State}");

            Console.Write($"AddCoin -> ");
            sm.Fire(Trigger.AddCoin);
            Console.WriteLine($"{sm.State}");

            Console.Write($"Push -> ");
            sm.Fire(Trigger.Push);
            Console.WriteLine($"{sm.State}");

            Console.Write($"Push -> ");
            sm.Fire(Trigger.Push);
            Console.WriteLine($"{sm.State}");

            Console.Write($"AddCoin -> ");
            sm.Fire(Trigger.AddCoin);
            Console.WriteLine($"{sm.State}");

            Console.Write($"Push -> ");
            sm.Fire(Trigger.Push);
            Console.WriteLine($"{sm.State}");

            Console.WriteLine("-----------");
        }

        static void SetUp(StateMachine<State, Trigger> machine)
        {
            machine.Configure(State.Locked)
                .Ignore(Trigger.Push)
                .Permit(Trigger.AddCoin, State.Unlocked);

            machine.Configure(State.Unlocked)
                .Ignore(Trigger.AddCoin)
                .Permit(Trigger.Push, State.Locked);
        }
    }

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
}
