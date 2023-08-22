using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.StatusClass;

namespace LegendofSparta.MonsterClass
{
    internal class Monster
    {
        public Status MonsterStatus; 
        
        public string Image { get; set; }

        public int Reward { get; set; }
        public Monster() 
        {
            MonsterStatus = new Status();
            MonsterStatus.Name = "";
            MonsterStatus.Hp = 0;
            MonsterStatus.MaxHp = 0;
            MonsterStatus.Atk = 0;
            MonsterStatus.Def = 0;
            Image = "";
            Reward = 0;
        }

        public string ShowMonsterState()
        {
            return $"{MonsterStatus.Name}  HP {MonsterStatus.Hp}/{MonsterStatus.MaxHp}  Atk {MonsterStatus.Atk}  Def {MonsterStatus.Def}";
        }

        public int TakeDamage(int playerDef)
        {
            Random random = new Random();
            int randomDamage = random.Next(MonsterStatus.Atk-5, MonsterStatus.Atk + 5);

            int actualAtk = randomDamage - playerDef; 
            actualAtk = Math.Max(actualAtk, 0);

            return actualAtk;
        }
    }
}
