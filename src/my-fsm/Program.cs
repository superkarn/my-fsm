using System;
using my_fsm.Models;

namespace my_fsm
{
    public class Program
    {

        static void Main(string[] args)
        {
            var ro = new RepairOrder();
    
            ro.GoToNextRoMode();
            ro.GoToNextRoMode();
            ro.Close();

            Console.WriteLine("-----------");
            
            ro.GoToNextPartStatus();

            Console.WriteLine("-----------");

            var line = new RepairOrderLine();
            ro.AddLine(line);
            line.Update();
            line.Update();
            line.Writeback();

            Console.WriteLine("-----------");
        }
    }
}
