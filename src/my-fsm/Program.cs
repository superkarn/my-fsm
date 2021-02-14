using System;

namespace my_fsm
{
    public class Program
    {

        static void Main(string[] args)
        {
            var ro = new RepairOrder();
    
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            
            Console.WriteLine("-----------");
            
            ro.GoToNextPartStatus();
            ro.GoToNextPartStatus();
            ro.GoToNextPartStatus();
            ro.GoToNextPartStatus();
            ro.GoToNextPartStatus();

            Console.WriteLine("-----------");
        }
    }
}
