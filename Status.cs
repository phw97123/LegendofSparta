using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendofSparta.StatusClass
{
    internal class Status
    {
        public string Name { get; set; }

         public Status()
         { 
            Name = "Unknown";
         }

        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("┌─────────────────────────────────┐");
                Console.WriteLine("│                           0.닫기│");
                Console.WriteLine("│            [상태창]             │");
                Console.WriteLine("│                                 │");
                Console.WriteLine($"│  {Name}             초보자        │");
                Console.WriteLine("│ =============================== │");
                Console.WriteLine("│  Lv. 01         HP 100/100      │");
                Console.WriteLine("│ =============================== │");
                Console.WriteLine("│  공격력 20                      │");
                Console.WriteLine("│  방어력 15                      │");
                Console.WriteLine("│   Gold  1000                    │");
                Console.WriteLine("│                                 │");
                Console.WriteLine("└─────────────────────────────────┘");

                string answer = Console.ReadLine();
                if(answer == "0")
                {
                    break; 
                }
            }
        }
    }
}
