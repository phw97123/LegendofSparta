using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.ItemClass; 

namespace LegendofSparta.StatusClass
{
    internal class Status
    {
        public string Name { get; set; }
        public int Level {  get; set; }
        public int Hp { get; set; }
        public int MaxHp 
        {  get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }

        public int Exp { get; set; }
        public int MaxExp {  get; set; }

        public Status()
         { 
            Name = "Unknown";
            Level = 0;
            Hp = 0; 
            MaxHp = 0;
            Mp  = 0;
            MaxMp = 0;
            Atk = 0;
            Def = 0;
            Gold = 0;
            Exp = 0;
            MaxExp = 0;
         }

        
    }
}
