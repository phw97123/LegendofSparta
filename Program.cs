﻿using LegendofSparta.GameManger;
using System.Runtime.CompilerServices;

namespace LegendofSparta  
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(42, 32);
           // Console.Clear(); 
            GameManager game = new GameManager();
            game.Run();
            
        }
    }
}