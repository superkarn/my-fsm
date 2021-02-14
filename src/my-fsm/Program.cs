using System;

namespace my_fsm
{
    public class Program
    {

        static void Main(string[] args)
        {
            var turnstile = new Turnstile();
    
            turnstile.AddCoin();
            turnstile.AddCoin();
            turnstile.Push();
            turnstile.Push();
            turnstile.AddCoin();
            turnstile.Push();

            Console.WriteLine("-----------");
        }
    }
}
