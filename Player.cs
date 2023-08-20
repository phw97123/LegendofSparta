using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.StatusClass;
using LegendofSparta.ItemClass;
using System.Globalization;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace LegendofSparta.PlayerClass
{
    class Player
     {
        public Status PlayerStatus;
        public List<Item> Inventory;
        public Player() 
        { 
            PlayerStatus = new Status();

            PlayerStatus.Level = 1;
            PlayerStatus.Hp = 100; 
            PlayerStatus.MaxHp = 200;
            PlayerStatus.Mp = 50; 
            PlayerStatus.MaxMp = 50;
            PlayerStatus.Atk = 20;
            PlayerStatus.Def = 15;
            PlayerStatus.Gold = 1000; 

            Inventory = new List<Item>();

            Item oldHead = new Item(ITEMTYPE.Head, "낡은투구",STATSTYPE.Def,"3", "세월이 느껴지는 투구", 300,true);
            Inventory.Add(oldHead);
            
            Item oldArmor = new Item(ITEMTYPE.Armor, "낡은갑옷",STATSTYPE.Def,"3", "세월이 느껴지는 갑옷", 300,true);
            Inventory.Add(oldArmor);

            Item oldSword = new Item(ITEMTYPE.Weapon, "낡은검", STATSTYPE.Atk,"3", "세월이 느껴지는 검", 300,true);
            Inventory.Add(oldSword);

            for(int i = 0; i<Inventory.Count; i++)
            {
                if (Inventory[i].IsEquip)
                {
                    switch(Inventory[i].StatsType)
                    {
                        case STATSTYPE.Atk:
                            PlayerStatus.Atk += int.Parse(Inventory[i].Stats);
                            break;
                        case STATSTYPE.Def:
                            PlayerStatus.Def += int.Parse(Inventory[i].Stats);
                            break;
                    }
                        
                }
            }
        }

        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                int hpColorPersent = (int)Math.Round(PlayerStatus.Hp / (double)PlayerStatus.MaxHp * 10);
                int hpRemove = 10 - (hpColorPersent + 1);

                if (hpRemove < 0)
                    hpRemove = -1;
                else if (hpRemove >= 9)
                    hpRemove = 8;

                string hpColor = "\x1b[41m" + "          " + "\x1b[0m" + "\x1b[0m";
                hpColor = hpColor.Remove(6, hpRemove + 1);

                int mpColorPersent = (int)Math.Round(PlayerStatus.Mp / (double)PlayerStatus.MaxMp * 10);
                int mpRemove = 10 - (mpColorPersent + 1);

                if (mpRemove < 0)
                    mpRemove = -1;
                else if (mpRemove >= 9)
                    mpRemove = 8;

                string mpColor = "\x1b[44m" + "          " + "\x1b[0m" + "\x1b[0m";
                mpColor = mpColor.Remove(6, mpRemove + 1);

                string equipAtk = ""; 
                string equipDef = ""; 
                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (Inventory[i].IsEquip)
                    {
                        switch (Inventory[i].StatsType)
                        {
                            case STATSTYPE.Atk:
                                equipAtk =  "\x1b[32m"+ PlayerStatus.Atk + "\x1b[0m" + "\x1b[0m";
                                break;
                            case STATSTYPE.Def:
                                equipDef = "\x1b[32m" + PlayerStatus.Def+ "\x1b[0m" + "\x1b[0m";
                                break;
                        }
                    }
                    else
                    {
                        for(int j = 0; j < Inventory.Count; j++)
                        {
                            if (!Inventory[j].IsEquip && Inventory[j].StatsType == STATSTYPE.Def)
                            {
                                equipDef = PlayerStatus.Def.ToString();
                            }
                        }
                        equipAtk = PlayerStatus.Atk.ToString();
                        
                    }
                }

                

                string format = "┌─────────────────────────────────┐\n" +
                             "│                           0.닫기│\n" +
                             "│            [상태창]             │\n" +
                             "│                                 │\n" +
                             "│  {0,-22}Lv.{1,3:D2}   │\n" +
                             "│ =============================== │\n" +
                             "│  HP {2,-3}/{3,-13}MP {4,3}/{5,-4}│\n" +
                             "│  {9,-32} {10,-24}│\n" +
                             "│ =============================== │\n" +
                             "│  공격력 {6,-24}│\n" +
                             "│  방어력 {7,-24}│\n" +
                             "│   Gold  {8,-24}|\n" +
                             "│                                 │\n" +
                             "└─────────────────────────────────┘";

                Console.WriteLine(string.Format(format,PlayerStatus.Name, PlayerStatus.Level, PlayerStatus.Hp, PlayerStatus.MaxHp, PlayerStatus.Mp, PlayerStatus.MaxMp, equipAtk, equipDef, PlayerStatus.Gold + "G", hpColor, mpColor));


                /*
                *문자열희 형식을 유지하면서 특정 부분만 변경
                string.Format: 보간 문자열 (-는 왼쪽 정렬, 변수의 출력공간 지정  
                */

                string answer = Console.ReadLine();
                if (answer == "0")
                {
                    break;
                }
            }
        }

        public void ShowInventory()
        {
            bool bEquipMode = false; 
           
            while(true)
            {
                Console.Clear(); 
                Console.WriteLine("┌─────────────────────────────────────────┐");
                Console.WriteLine("│                                   0.닫기│");
                Console.WriteLine("│             [인벤토리]                  │");
                Console.WriteLine("│                                         │");

                for (int i = 0; i < Inventory.Count; i++)
                {
                    string firstLine = $"| {(Inventory[i].IsEquip ? "[E]" : " ")} {(bEquipMode ? (i + 1) + "." : " ")}{Inventory[i].Name,-20}{Inventory[i].StatsType,3} +{Inventory[i].Stats,-5} |";
                    Console.WriteLine(string.Format(firstLine));
                    Console.WriteLine($"| \x1b[2;37m {Inventory[i].Description,-30}\x1b[0m|");

                    Console.WriteLine("│ ======================================  │");
                    Console.WriteLine("│                                         │");
                }


                Console.WriteLine($"│   {(bEquipMode ? "          ":"1.장착관리")}                            │");
                Console.WriteLine("│                                         │");
                Console.WriteLine("└─────────────────────────────────────────┘");
                Console.Write(">> "); 

                string answer = Console.ReadLine();
                int select;
                bool bVaild = int.TryParse(answer, out select);

                if(bVaild)
                {
                    if (bEquipMode == false)
                    {
                        if (select == 0)
                            break;
                        else if (select == 1)
                        {
                            bEquipMode = true;
                        }
                    }
                    else
                    {
                        if(select != 0  && Inventory.Count >= select)
                        {
                            //Inventory[select-1].IsEquip = Inventory[select-1].IsEquip ? false : true;

                            if (Inventory[select-1].IsEquip)
                            {
                                switch (Inventory[select-1].StatsType)
                                {
                                    case STATSTYPE.Atk:
                                        PlayerStatus.Atk -= int.Parse(Inventory[select-1].Stats);
                                        break;
                                    case STATSTYPE.Def:
                                        PlayerStatus.Def -= int.Parse(Inventory[select-1].Stats);
                                        break;
                                }
                                Inventory[select-1].IsEquip = false;
                            }
                            else
                            {
                                switch (Inventory[select - 1].StatsType)
                                {
                                    case STATSTYPE.Atk:
                                        PlayerStatus.Atk += int.Parse(Inventory[select - 1].Stats);
                                        break;
                                    case STATSTYPE.Def:
                                        PlayerStatus.Def += int.Parse(Inventory[select - 1].Stats);
                                        break;
                                }
                                Inventory[select - 1].IsEquip = true;
                            }


                        }
                        else if(select == 0)
                        {
                            bEquipMode = false;
                        }
                        else
                        {
                            Console.WriteLine("잘못 입력하셨습니다."); 
                            Thread.Sleep(500);
                        }
                    }
                }
            }

            /* 생각하기
             제일 긴 문자열의 길이에 맞춰 출력하고 한글의 크기랑 같은 띄어쓰기로 빈공간을 채움      
            */

        }
        



    }
}
