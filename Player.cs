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
using System.ComponentModel;

namespace LegendofSparta.PlayerClass
{
    class Player
     {
        public Status PlayerStatus;
        public List<Item> Inventory;

        public Item? equipWeapon { get; set; }
        public Item? equipHead { get; set; }
        public Item? equipArmor { get; set; }

        ITEMSORT itemsort; //아이템 정렬방식 enum
        public Player() 
        { 
            PlayerStatus = new Status();

            //기본 스탯
            PlayerStatus.Level = 1;
            PlayerStatus.Hp = 100; 
            PlayerStatus.MaxHp = 200;
            PlayerStatus.Mp = 50; 
            PlayerStatus.MaxMp = 50;
            PlayerStatus.Atk = 20;
            PlayerStatus.Def = 15;
            PlayerStatus.Gold = 1000; 

            Inventory = new List<Item>();

            itemsort = ITEMSORT.Name; 

            equipWeapon = new Item();
            equipHead = new Item();
            equipArmor = new Item();

            //초기 아이템
            Item oldHead = new Item(ITEMTYPE.Head, "낡은투구",STATSTYPE.Def,"3", "세월이 느껴지는 투구", 300,true);
            Inventory.Add(oldHead);
            
            Item oldArmor = new Item(ITEMTYPE.Armor, "낡은갑옷",STATSTYPE.Def,"5", "세월이 느껴지는 갑옷", 300,true);
            Inventory.Add(oldArmor);

            Item oldSword = new Item(ITEMTYPE.Weapon, "낡은검", STATSTYPE.Atk,"3", "세월이 느껴지는 검", 300,true);
            Inventory.Add(oldSword);

            Item oldspear= new Item(ITEMTYPE.Weapon, "낡은창", STATSTYPE.Atk, "5", "세월이 느껴지는 창", 300, false);
            Inventory.Add(oldspear);


            //초기 아이템 능력치 추가
            //for (int i = 0; i<Inventory.Count; i++)
            //{
            //    if (Inventory[i].IsEquip)
            //    {
            //        switch(Inventory[i].StatsType)
            //        {
            //            case STATSTYPE.Atk:
            //                PlayerStatus.Atk += int.Parse(Inventory[i].Stats);
            //                break;
            //            case STATSTYPE.Def:
            //                PlayerStatus.Def += int.Parse(Inventory[i].Stats);
            //                break;
            //        }
                        
            //    }
            //}

            //초기 아이템 장착
            for(int i = 0; i<Inventory.Count; i++)
            {
                if (Inventory[i].IsEquip)
                {
                    switch(Inventory[i].ItemType)
                    {
                        case ITEMTYPE.Weapon:
                            equipWeapon = Inventory[i];
                            PlayerStatus.Atk += int.Parse(Inventory[i].Stats); 
                            break;
                        case ITEMTYPE.Head:
                            equipHead = Inventory[i];
                            PlayerStatus.Def += int.Parse(Inventory[i].Stats);
                            break;
                        case ITEMTYPE.Armor:
                            equipArmor = Inventory[i];
                            PlayerStatus.Def += int.Parse(Inventory[i].Stats);
                            break;
                    }
                }
            }
        }

        //상태창
        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();

                //HP바 % 계산 
                int hpColorPersent = (int)Math.Round(PlayerStatus.Hp / (double)PlayerStatus.MaxHp * 10);
                int hpRemove = 10 - (hpColorPersent + 1);

                if (hpRemove < 0)
                    hpRemove = -1;
                else if (hpRemove >= 9)
                    hpRemove = 8;

                string hpColor = "\x1b[41m" + "          " + "\x1b[0m" + "\x1b[0m";
                hpColor = hpColor.Remove(6, hpRemove + 1);

                //MP바 % 계산
                int mpColorPersent = (int)Math.Round(PlayerStatus.Mp / (double)PlayerStatus.MaxMp * 10);
                int mpRemove = 10 - (mpColorPersent + 1);

                if (mpRemove < 0)
                    mpRemove = -1;
                else if (mpRemove >= 9)
                    mpRemove = 8;

                string mpColor = "\x1b[44m" + "          " + "\x1b[0m" + "\x1b[0m";
                mpColor = mpColor.Remove(6, mpRemove + 1);

                //아이템의 효과로 능력치가 올라가면 능력치 츨력 색 변경  
                string equipAtk = ""; 
                string equipDef = ""; 

                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (equipWeapon == Inventory[i] || equipArmor == Inventory[i] || equipHead == Inventory[i]) //아이템 장착했다면 
                    {
                        switch (Inventory[i].StatsType)
                        {
                            case STATSTYPE.Atk:
                                equipAtk = "\x1b[32m"+ PlayerStatus.Atk + "\x1b[0m" + "\x1b[0m";
                                break;
                            case STATSTYPE.Def:
                                equipDef = "\x1b[32m" + PlayerStatus.Def+ "\x1b[0m" + "\x1b[0m";
                                break;
                        }
                    }
                    else //장착하지 않았다면  
                    {
                        if(equipWeapon == null)
                        {
                            switch (Inventory[i].StatsType)
                            {
                                case STATSTYPE.Atk:
                                    equipAtk = PlayerStatus.Atk.ToString();
                                    break;
                                case STATSTYPE.Def:
                                    equipDef = PlayerStatus.Def.ToString();
                                    break;
                            }
                        }
                        
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

                string? answer = Console.ReadLine();
                if (answer == "0")
                {
                    break;
                }
            }
        }

        //인벤토리 
        public void ShowInventory()
        {
            
            //int totalWidth = 43; //전체 문자열 너비 
          
            //장착모드 관리
            bool bEquipMode = false;
            //아이템 정렬 관리 
            bool bItemSort = false;
            //모드 관리 문자열
            string modeChage = "1.장착모드 2.아이템 정렬";

            string equipStr = " "; 

            while (true)
            {
                switch (itemsort) //인벤토리 정렬
                {
                    case ITEMSORT.Name:
                        Inventory = Inventory.OrderBy(item=>item.Name).ToList();
                        break; 
                    case ITEMSORT.Equip:
                        Inventory = Inventory.OrderBy(item=>(bool)item.IsEquip ? 0:1).ToList();
                        break;
                    case ITEMSORT.Atk:
                        Inventory = Inventory.OrderBy(item => item.StatsType == STATSTYPE.Atk ? 0 : 1).ThenByDescending(item => int.Parse(item.Stats)).ToList();
                        break;
                    case ITEMSORT.Def:
                        Inventory = Inventory.OrderBy(item => item.StatsType == STATSTYPE.Def ? 0 : 1).ThenByDescending(item => int.Parse(item.Stats)).ToList(); 
                        break;
                }

                /*
                List . OrderBy () 원하는 방법으로 정렬할 수 있음 
                Inventory = Inventory.OrderBy(item => item.StatsType == STATSTYPE.Atk ? 0 : 1).ThenByDescending(item => int.Parse(item.Stats)).ToList();

                item.StatsType == STATSTYPE.Atk ? 0 : 1 
                0과 1은 키 값을 정해주는 것으로 참이면 0의 키값을 거짓이면 1의 키값을 줘서 오름차순으로 정렬한다 
                ThenByDescending : 이전 조건에 따라 정렬된 결과를 바탕으로, 지정된 변수 값을 내림차순으로 정렬한다       
                 */

                Console.Clear();
                Console.WriteLine("┌─────────────────────────────────────────┐");
                Console.WriteLine("│                                   0.닫기│");
                Console.WriteLine("│             [인벤토리]                  │");
                Console.WriteLine("│                                         │");

                //Console.WriteLine("┌──────────────────────────────────────────────────────┐");
                //Console.WriteLine("│                     [인벤토리]                       │");
                //Console.WriteLine("│                                                      │");
                //Console.WriteLine("│ ─────────────────────────────────────────────────────│");

                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (Inventory[i].IsEquip) //장착중
                    {
                        if (Inventory[i] == equipWeapon || Inventory[i] == equipHead || Inventory[i] == equipArmor) //장착되어있는 아이템이 같으면
                        {
                            equipStr = "[E]"; 
                        } 
                        else
                        {
                            equipStr = " ";
                            Inventory[i].IsEquip = false;
                        }
                    }
                    else
                    {
                        equipStr = " "; 
                    }

                    string firstLine = $"| {equipStr} {(bEquipMode ? (i + 1) + "." : " ")}{Inventory[i].Name,-20}{Inventory[i].StatsType,3} +{Inventory[i].Stats,-5} |";
                    Console.WriteLine(string.Format(firstLine));
                    Console.WriteLine($"| \x1b[2;37m {Inventory[i].Description,-30}\x1b[0m|");

                    //string itemName = Inventory[i].Name.PadRight(30);
                    //string itemStats = Inventory[i].Stats.PadRight(20);
                    //string itemDescription = Inventory[i].Description.PadRight(50);

                    //string itemLine = $"│ {itemName} {itemStats}  │";
                    //string DescriptionLine = $"│ {itemDescription} │";

                    //Console.WriteLine(itemLine);
                    //Console.WriteLine(itemDescription);

                    //string itemLine = string.Format("│ {0,-30} {1,-20} │", Inventory[i].Name, Inventory[i].Stats);
                    //string desriptionLine = string.Format("│ {0,-50} │", Inventory[i].Description);
                    //Console.WriteLine(itemLine);
                    //Console.WriteLine(desriptionLine);

                    Console.WriteLine("│ ======================================  │");
                    Console.WriteLine("│                                         │");
                }


                Console.WriteLine($"│ {modeChage,-31}│");
                Console.WriteLine("│                                         │");
                Console.WriteLine("└─────────────────────────────────────────┘");
                Console.Write(">> "); 

                string? answer = Console.ReadLine();
                int select;
                bool bVaild = int.TryParse(answer, out select);

                //유효한 입력일 때 
                if(bVaild)
                {
                    //장착모드가 아닐 때
                    if (bEquipMode == false && bItemSort == false)
                    {
                        if (select == 0)
                        {
                            break;
                        }
                        else if (select == 1)
                        {
                            bEquipMode = true;
                            modeChage = " ";
                        }
                        else if(select == 2)
                        {
                            bItemSort = true;
                            modeChage = "1.이름 2.장착순 3. 공격력 4.방어력";
                        }
                        else
                        {
                            Console.WriteLine("잘못 입력하셨습니다.");
                            Thread.Sleep(500);
                        }
                        
                    }
                    else if(bEquipMode == true && !bItemSort) //장착모드일 때 
                    {
                        //아이템 선택
                        //if(select != 0  && Inventory.Count >= select)
                        //{
                        //    //Inventory[select-1].IsEquip = Inventory[select-1].IsEquip ? false : true;
                        //    //아이템 장착중이라면 장착 해제
                        //    if (Inventory[select-1].IsEquip)
                        //    {
                        //        switch (Inventory[select-1].StatsType)
                        //        {
                        //            //아이템 장착해제 후 능력치 조절
                        //            case STATSTYPE.Atk:
                        //                PlayerStatus.Atk -= int.Parse(Inventory[select-1].Stats);
                        //                break;
                        //            case STATSTYPE.Def:
                        //                PlayerStatus.Def -= int.Parse(Inventory[select-1].Stats);
                        //                break;
                        //        }
                        //        Inventory[select-1].IsEquip = false;
                        //    }
                        //    else //장착완료
                        //    {
                        //        //아이템 장착 후 능력치 조절  
                        //        switch (Inventory[select - 1].StatsType)
                        //        {
                        //            case STATSTYPE.Atk:
                        //                PlayerStatus.Atk += int.Parse(Inventory[select - 1].Stats);
                        //                break;
                        //            case STATSTYPE.Def:
                        //                PlayerStatus.Def += int.Parse(Inventory[select - 1].Stats);
                        //                break;
                        //        }
                        //        Inventory[select - 1].IsEquip = true;
                        //    }
                        //}
                        //else if(select == 0) //장착모드 해제
                        //{
                        //    bEquipMode = false;
                        //    modeChage = "1.장착모드 2.아이템 정렬";
                        //}
                        //else
                        //{
                        //    Console.WriteLine("잘못 입력하셨습니다."); 
                        //    Thread.Sleep(500);
                        //}

                        if (select != 0 && Inventory.Count >= select)
                        {
                            //아이템 장착중이라면 장착 해제
                            if (Inventory[select - 1].IsEquip)
                            {
                                switch (Inventory[select - 1].StatsType)
                                {
                                    //아이템 장착해제 후 능력치 조절
                                    case STATSTYPE.Atk:
                                        PlayerStatus.Atk -= int.Parse(Inventory[select - 1].Stats);
                                        equipWeapon = null;
                                        break;
                                    case STATSTYPE.Def:
                                        PlayerStatus.Def -= int.Parse(Inventory[select - 1].Stats);
                                        if (Inventory[select - 1].ItemType == ITEMTYPE.Armor)
                                            equipArmor = null;
                                        else
                                            equipHead = null; 
                                        break;
                                }
                                Inventory[select - 1].IsEquip = false;
                            }
                            else //장착완료
                            {
                                //아이템 장착 후 능력치 조절  
                                switch (Inventory[select - 1].StatsType)
                                {
                                    case STATSTYPE.Atk:
                                        if(equipWeapon != null)
                                            PlayerStatus.Atk -= int.Parse(equipWeapon.Stats);
                                        equipWeapon = Inventory[select - 1];
                                        PlayerStatus.Atk += int.Parse(Inventory[select - 1].Stats);
                                        break;
                                    case STATSTYPE.Def:
                                       
                                        if (Inventory[select - 1].ItemType == ITEMTYPE.Armor)
                                        {
                                            if (equipArmor != null)
                                                PlayerStatus.Def -= int.Parse(equipArmor.Stats);
                                            equipArmor = Inventory[select - 1];
                                        }
                                        else
                                        {
                                            if (equipHead != null)
                                                PlayerStatus.Def -= int.Parse(equipHead.Stats);
                                            equipHead = Inventory[select - 1];
                                        }
                                        PlayerStatus.Def += int.Parse(Inventory[select - 1].Stats);
                                        break;
                                }
                                Inventory[select - 1].IsEquip = true;
                            }
                        }
                        else if (select == 0) //장착모드 해제
                        {
                            bEquipMode = false;
                            modeChage = "1.장착모드 2.아이템 정렬";
                        }
                        else
                        {
                            Console.WriteLine("잘못 입력하셨습니다.");
                            Thread.Sleep(500);
                        }
                    }
                    else if(bItemSort == true && bEquipMode == false) //아이템 정렬모드
                    {
                        if(select >0 && select<5) //인벤토리 정렬 설정
                        {
                            switch(select)
                            {
                                case 1:
                                    itemsort = ITEMSORT.Name; 
                                    Console.WriteLine("이름순.");
                                    Thread.Sleep(500);
                                    break; 
                                case 2:
                                    itemsort = ITEMSORT.Equip;
                                    Console.WriteLine("장착순"); 
                                    Thread.Sleep(500);
                                    break;
                                case 3:
                                    itemsort = ITEMSORT.Atk;
                                    Console.WriteLine("공격력");
                                    Thread.Sleep(500);
                                    break ; 
                                case 4:
                                    itemsort = ITEMSORT.Def;
                                    Console.WriteLine("방어력");
                                    Thread.Sleep(500);
                                    break;

                            }
                        }
                        else if(select == 0)
                        {
                            bItemSort = false;
                            modeChage = "1.장착모드 2.아이템 정렬";
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다.");
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Thread.Sleep(500);
                }
            }
            


            /* 생각하기
             제일 긴 문자열의 길이에 맞춰 출력하고 한글의 크기랑 같은 띄어쓰기로 빈공간을 채움      
            */

        }
    }
}
