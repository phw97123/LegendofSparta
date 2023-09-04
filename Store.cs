using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.ItemClass;
using LegendofSparta.PlayerClass; 


namespace LegendofSparta
{
    [Serializable]
    internal class Store
    {
        public List<Item> StoreItem;
       
        public Store()
        {
            //아이템 생성 및 추가
            StoreItem = new List<Item>();
            Item spartaSword = new Item(ITEMTYPE.Weapon, "스파르타인의 대검", STATSTYPE.Atk, "30", "이 검이라면 전설에 가까워질듯 하다", 3000, false);
            Item spartaHead = new Item(ITEMTYPE.Head, "스파르타인의 투구", STATSTYPE.Def, "30", "이 투구만 있다면 나도 전설이다", 3000, false);
            Item spartaArmor = new Item(ITEMTYPE.Armor, "스파르타인의 갑옷", STATSTYPE.Def, "50", "이 갑옷만 있다면 야 너두 할 수있어", 5000, false);
            Item GoldCopy = new Item(ITEMTYPE.Gold, "1000G", STATSTYPE.Gold, "1000", "골드를 복사할 수 있는 치트", 0, false);
            StoreItem.Add(spartaSword);
            StoreItem.Add(spartaHead);
            StoreItem.Add(spartaArmor);
            StoreItem.Add(GoldCopy);
        }

       //상점 입장 화면 
        public void StartStroe(Player player)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("                     [상점]                      ");
                Console.WriteLine("        뭘 사려고? 아니면 뭘 좀 팔아볼텐가?        ");
                Console.WriteLine();
                Console.WriteLine("            0.나가기  1.구매  2.판매");
                Console.WriteLine();
                Console.Write(">> ");
                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);
                if (bValid)
                {
                    if (select == 0)
                    {
                        break; 
                    }
                    else if(select == 1)
                    {
                        ShowPurchase(player);
                    }
                    else if(select == 2)
                    {
                        ShowSale(player);
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다");
                        Thread.Sleep(500);
                    }

                }
                else 
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Thread.Sleep(500); 
                }
            }
            
        }

        //상점 구매 탭
        public void ShowPurchase(Player player)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(); 
                Console.WriteLine("                     [상점]                      ");
                Console.WriteLine("              최고의 물건들만 있다네.             ");
                Console.WriteLine();

                for (int i = 0; i < StoreItem.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}.{StoreItem[i].Name,-27}{StoreItem[i].StatsType,4} +{StoreItem[i].Stats,5}");
                    Console.WriteLine($"   {StoreItem[i].Description,-25}{StoreItem[i].Price,5}G");
                    Console.WriteLine();
                }

                Console.WriteLine(" 0.나가기");
                Console.WriteLine();
                Console.WriteLine($" 현재 골드 : {player.PlayerStatus.Gold}G"); 
                Console.Write(">> ");
                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);
                if (bValid)
                {
                    if (select == 0)
                    {
                        break;
                    }
                    else if(select != 0  && select <= StoreItem.Count) //상점에 있는 아이템을 선택
                    {
                        //돈이 부족하지 않고, 골드를 더 해주는 치트키 아이템이 아니고, 인벤토리에 빈 슬롯이 있다면
                        if (StoreItem[select-1].Price<=player.PlayerStatus.Gold && StoreItem[select-1].ItemType != ITEMTYPE.Gold && player.Inventory.Count < player.itemLimit)
                        {
                            player.PlayerStatus.Gold -= StoreItem[select - 1].Price;
                            player.Inventory.Add(StoreItem[select-1]);
                            StoreItem.RemoveAt(select-1);
                            Console.WriteLine("구매완료");
                        }
                        //아이템 타입이 골드 타입이라면 
                        else if(StoreItem[select - 1].ItemType == ITEMTYPE.Gold)
                        {
                            player.PlayerStatus.Gold += int.Parse(StoreItem[select - 1].Stats);
                            Console.WriteLine("구매완료");
                        }
                        else if(player.Inventory.Count == player.itemLimit)
                        {
                            Console.WriteLine("가방이 꽉 찼습니다.");
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine("잔액이 부족합니다.");
                            Thread.Sleep(500); 
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다");
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Thread.Sleep(500);
                }
            }
        }

        //상점 판매 탭
        public void ShowSale(Player player)
        {
            while(true)
            { 
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine("                     [상점]                      ");
                Console.WriteLine("          특별히 비싸게 사주도록 하지.         ");
                Console.WriteLine();

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}.{player.Inventory[i].Name,-25}{player.Inventory[i].StatsType,4} +{player.Inventory[i].Stats,-5}");
                    Console.WriteLine($"   {player.Inventory[i].Description,-25}{(int)(player.Inventory[i].Price*0.5f),5}G");
                    Console.WriteLine();
                }

                Console.WriteLine(" 0.나가기");
                Console.WriteLine();
                Console.WriteLine($" 현재 골드 : {player.PlayerStatus.Gold}G");
                Console.Write(">> ");
                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);
                if (bValid)
                {
                    if (select == 0)
                    {
                        break;
                    }
                    else if (select != 0 && select <= player.Inventory.Count) //아이템 선택시
                    {
                        if (player.Inventory[select-1] == player.equipArmor || player.Inventory[select - 1] == player.equipHead|| player.Inventory[select - 1] == player.equipWeapon) //장착된 아이템이면 판매 불가
                        {
                            Console.WriteLine("장착중인 아이템은 판매를 할 수 없습니다.");
                            Thread.Sleep(500); 
                        }
                        else
                        { 
                            // 구매 가격의 50% 판매 가능
                            player.PlayerStatus.Gold += (int)(player.Inventory[select - 1].Price*0.5f);
                            StoreItem.Add(player.Inventory[select - 1]);
                            player.Inventory.RemoveAt(select - 1);
                            Console.WriteLine("판매 완료");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다");
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Thread.Sleep(500);
                }
            }
            
        }
    }
}
