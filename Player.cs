using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.StatusClass; 

namespace LegendofSparta.PlayerClass
{
     class Player
     {
        public Status PlayerStatus; 

        public Player() 
        { 
            PlayerStatus = new Status();
        }
     }
}
