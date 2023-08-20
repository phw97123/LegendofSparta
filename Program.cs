using LegendofSparta.GameManger;
using System.Runtime.CompilerServices;

namespace LegendofSparta  
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();
            game.Run();

            //int hp = 100;
            //int maxhp = 200;

            //int remove = (int)Math.Round(hp / (double)maxhp * 10);
            //int removepersent = 10 - (remove + 1);
            //if (removepersent < 0)
            //    removepersent = -1;
            //else if(removepersent >=9)
            //    removepersent = 8;

            //string maxHP = "\x1b[41m          \x1b[0m";
            //string HP = "\x1b[41m          \x1b[0m";

            //HP = HP.Remove(6,removepersent+1);
            
            //Console.WriteLine(maxHP);
            //Console.WriteLine(HP);
            //Console.WriteLine("\x1b[0m");
            //Console.WriteLine(removepersent);
            
            //Console.WriteLine(HP);

           


            //if (hpColorPersent == 10)
            //    hpColorPersent = 9;
            //else if (hpColorPersent <= 0)
            //    hpColorPersent = 9;
            //else
            //    hpColorPersent -= 1;
        }
    }
}