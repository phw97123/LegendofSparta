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
using System.Collections;

namespace LegendofSparta.PlayerClass
{
    class Player
    {
        public Status PlayerStatus;
        public List<Item> Inventory;
        public int itemLimit = 5;

        //전투 승리시
        public bool bVictory = false;
        //처치한 몬스터 이름 저장
        public string victoryMonster = ""; 

        //장착된 아이템 
        public Item? equipWeapon { get; set; }
        public Item? equipHead { get; set; }
        public Item? equipArmor { get; set; }

        ITEMSORT itemsort; //아이템 정렬방식 enum
        public Player() 
        { 
            PlayerStatus = new Status();

            //기본 스탯
            PlayerStatus.Level = 1;
            PlayerStatus.Hp = 50; 
            PlayerStatus.MaxHp = 100;
            PlayerStatus.Mp = 50; 
            PlayerStatus.MaxMp = 50;
            PlayerStatus.Atk = 20;
            PlayerStatus.Def = 15;
            PlayerStatus.Gold = 1000;
            PlayerStatus.Exp = 0;
            PlayerStatus.MaxExp = 50; 

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
                            equipAtk = PlayerStatus.Atk.ToString();
                        }
                        else if(equipArmor ==null && equipHead == null)
                        {
                            equipDef = PlayerStatus.Def.ToString();
                        }
                        
                    }
                }

                string format = "┌─────────────────────────────────┐\n" +
                                "│                           0.닫기│\n" +
                                "│            [상태창]             │\n" +
                                "│                                 │\n" +
                                "│  {0,-22}Lv.{1,3:D2}   │\n" +
                                "│ =============================== │\n" +
                                "│                                 │\n" +
                                "│  {2,-32} {3,-24}│\n" +
                                "│ =============================== │\n" +
                                "│                                 │\n" +
                                "│                                 │\n" +
                                "│                                 │\n" +
                                "│                                 │\n" +
                                "│                                 │\n" +
                                "└─────────────────────────────────┘";
              
                

                Console.WriteLine(string.Format(format,PlayerStatus.Name,PlayerStatus.Level,hpColor,mpColor));
                Console.SetCursorPosition(3, 6);
                string HPMP= $"HP {PlayerStatus.Hp}/{PlayerStatus.MaxHp}          MP {PlayerStatus.Mp}/{PlayerStatus.MaxMp}";
                Console.WriteLine(HPMP);
                Console.SetCursorPosition(3, 9);
                Console.WriteLine($"공격력 {equipAtk}");
                Console.SetCursorPosition(3, 10);
                Console.WriteLine($"방어력 {equipDef}");
                Console.SetCursorPosition(3, 11);
                Console.WriteLine($"Gold {PlayerStatus.Gold}G");
                Console.SetCursorPosition(3, 12);
                Console.WriteLine($"Exp {PlayerStatus.Exp}/{PlayerStatus.MaxExp}");
                Console.SetCursorPosition(0, 15);
                Console.Write(">> ");

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

            string equipStr = ""; 

            while (true)
            {
                //인벤토리 정렬
                switch (itemsort)
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

                string statsDisplay ="┌─────────────────────────────────────────────────────┐\n"+
                                     "│                     [인벤토리]                0.닫기│\n"+
                                     "│                                                     │\n"+
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "│                                                     │\n" +
                                     "└─────────────────────────────────────────────────────┘\n";

                Console.WriteLine(statsDisplay);
               
                for (int i = 0; i < itemLimit; i++)
                {
                    if(i<Inventory.Count)
                    {
                        if (Inventory[i].IsEquip) //장착중
                        {
                            //장착되어있는 아이템이 같으면
                            if (Inventory[i] == equipWeapon || Inventory[i] == equipHead || Inventory[i] == equipArmor) 
                            {
                                equipStr = "[E]";
                            }
                            else
                            {
                                equipStr = "";
                                Inventory[i].IsEquip = false;
                            }
                        }
                        else
                        {
                            equipStr = "";
                        }

                        Console.SetCursorPosition(3, 3 + 4 * i);
                        string firstLine = $"{equipStr} {(bEquipMode ? (i + 1) + "." : "")}{Inventory[i].Name}{Inventory[i].StatsType,15} +{Inventory[i].Stats} ";
                        Console.WriteLine(string.Format(firstLine));
                        Console.SetCursorPosition(3, 4 + 4 * i);
                        Console.WriteLine($"\x1b[2;37m {Inventory[i].Description}\x1b[0m");
                        Console.SetCursorPosition(3, 5 + 4 * i);
                        Console.WriteLine("-----------------------------------------------");
                    }
                    else
                    {
                        Console.SetCursorPosition(3, 5 + 4 * i);
                        Console.WriteLine("-----------------------------------------------");
                    }
                }
                Console.SetCursorPosition(5, 23);

                Console.WriteLine($"{modeChage}");
                Console.SetCursorPosition(1, 26);
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
                        if (select != 0 && Inventory.Count >= select) //장착할 아이템 선택 
                        {
                            //아이템 장착중이라면 장착 해제
                            if (Inventory[select - 1].IsEquip)
                            {
                                switch (Inventory[select - 1].StatsType)
                                {
                                    //아이템 장착 해제 후 능력치 조절
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
        }

        //일반 공격
        public int TakeDamage(int monsterDef)
        {
            Random random = new Random();
            int randomDamage = random.Next(PlayerStatus.Atk-5, PlayerStatus.Atk+5);

            int actualAtk = randomDamage - monsterDef;
            actualAtk = Math.Max(actualAtk, 0);

            return actualAtk; 
        }

        //치명타 공격
        public int CriticalTakeDamage(int monsterDef)
        {
            PlayerStatus.Mp -= 10; 

            Random rand = new Random();
            int randomDamage = rand.Next(PlayerStatus.Atk + 30, PlayerStatus.Atk+50);

            int actualAtk = randomDamage - monsterDef;
            actualAtk = Math.Max(actualAtk, 0); 

            return actualAtk;
        }

        //레벨업 구현
        public void LevelUpWithExp()
        {
            //플레이어의 경험치가 최대 경험치보다 같거나 많을 경우 레벨을 계속 올려주기 위한 반복문
            while (true)
            {
                if (PlayerStatus.Exp >= PlayerStatus.MaxExp)
                {
                    int remaining = PlayerStatus.Exp - PlayerStatus.MaxExp;
                    PlayerStatus.Level+=1;
                    PlayerStatus.Exp = 0;
                    PlayerStatus.MaxExp += 20;
                    PlayerStatus.MaxHp += 20;
                    PlayerStatus.MaxMp += 20;
                    PlayerStatus.Atk += 5;
                    PlayerStatus.Def += 5;
                    PlayerStatus.Exp += remaining;
                    Console.WriteLine($"{PlayerStatus.Name}의 레벨이 1 올랐다"); 
                }
                else
                {
                    break;
                }
            }
        }

    }
}
