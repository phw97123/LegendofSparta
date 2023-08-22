using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using LegendofSparta.MonsterClass;
using LegendofSparta.PlayerClass; 

namespace LegendofSparta.GameManger
{
     internal class GameManager
     {
        Player player = new Player();
        Store store = new Store();
        Dungeon dungeon = new Dungeon();

        public void TextOutput(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string text1 = text.Substring(i, 1);
                Console.Write(text1);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }

        void GameStartScene()
        {
            Console.WriteLine(@"

                                                       /$$              
                                                      | $$              
              /$$$$$$$  /$$$$$$   /$$$$$$   /$$$$$$  /$$$$$$    /$$$$$$ 
             /$$_____/ /$$__  $$ |____  $$ /$$__  $$|_  $$_/   |____  $$
            |  $$$$$$ | $$  \ $$  /$$$$$$$| $$  \__/  | $$      /$$$$$$$
             \____  $$| $$  | $$ /$$__  $$| $$        | $$ /$$ /$$__  $$5
             /$$$$$$$/| $$$$$$$/|  $$$$$$$| $$        |  $$$$/|  $$$$$$$5
            |_______/ | $$____/  \_______/|__/         \___/   \_______/
                      | $$                                              
                      | $$                                              
                      |__/                                              
                                                    의 전설


                             아무 키나 눌러서 실행...

            ");

            Console.WriteLine("                        ");
            Console.ReadKey();

            CreateCharacter(); 
            
        }

        void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine(" 플레이어의 이름을 정해주세요.");
            Console.WriteLine("┌─────────────────────────────────┐");
            Console.WriteLine("│                                 │");
            Console.WriteLine("│    이름:                        │");
            Console.WriteLine("│                                 │");
            Console.WriteLine("└─────────────────────────────────┘");

            //string playerNameSet = "플레이어의 이름을 정해주세요.\n" +
            //                       "┌─────────────────────────────────┐\n" +
            //                       "│                                 │\n" +
            //                       "│    이름:                        │\n" +
            //                       "│                                 │\n" +
            //                       "└─────────────────────────────────┘\n";

            //CenterWriteLine(playerNameSet);

            Console.SetCursorPosition(11, Console.CursorTop - 3);

            player.PlayerStatus.Name = Console.ReadLine();

            CheckCharacterCreation(); 
        }

        void CheckCharacterCreation()
        {
            int totalWidth = 34; // 전체 문자열 너비 
            int padding = (totalWidth - player.PlayerStatus.Name.Length - 2) / 2; // 가운데 정렬을 위한 패딩 계산 (|가 2개 있으니 -2)

            while (true)
            {
                Console.Clear(); 

                Console.WriteLine("이 이름으로 하시겠습니까?");
                Console.WriteLine("┌─────────────────────────────────┐");
                Console.WriteLine("│                                 │");
                Console.WriteLine($"|{player.PlayerStatus.Name.PadLeft(padding + player.PlayerStatus.Name.Length).PadRight(totalWidth - 1)}|");
                //-1을 한 이유는 마지막 '|' 문자를 포함하여 전체 길이에 맞게 패딩을 적용
                Console.WriteLine("│                                 │");
                Console.WriteLine("|              Y / N              |");
                Console.WriteLine("│                                 │");
                Console.WriteLine("└─────────────────────────────────┘");
                Console.Write(">> "); 

                string? answer = Console.ReadLine();
                if (answer == "y" || answer == "y")
                {
                    Console.Clear();
                    //Console.WriteLine("캐릭터 생성 완료...");
                    TextOutput("캐릭터 생성 중..."); 
                    Thread.Sleep(500);
                    TextOutput("캐릭터 생성 완료 !");
                    //Console.WriteLine("마을로 들어가는 중...");
                    TextOutput("마을로 들어가는 중...");
                    Thread.Sleep(1000);
                    VillageScene(); 
                    break; 
                }
                else if (answer == "n" || answer == "N")
                {
                    CreateCharacter();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다.");
                    Thread.Sleep(500);
                }
            }
           /*
           PadLeft,Right() : 문자열을 지정한 길이로 패딩한다
           */
        }

        void VillageScene()
        {
            while(true)
            {
                
                if (player.PlayerStatus.Hp <= 0)
                {
                    PlayerDie(); 
                    player.PlayerStatus.Hp = (int)(player.PlayerStatus.MaxHp * 0.1);
                }
                else if(player.bVictory == true)
                {
                    PlayerVictory(player.victoryMonster);
                    player.bVictory = false;
                    player.victoryMonster = "";
                }


                Console.Clear();
                Console.WriteLine($"안녕하세요 {player.PlayerStatus.Name}님");
                Console.WriteLine("여기는 전설이 되기 위해 많은 모험가들이 모이는 마을입니다");
                Console.WriteLine("이곳에서 전설이 되기 위해 준비의 준비를 하세요");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------");

                Console.WriteLine();
                Console.WriteLine("1.상태창");
                Console.WriteLine("2.인벤토리");
                Console.WriteLine("3.상점");
                Console.WriteLine("4.던전"); 
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("원하는 행동을 입력해주세요.");
                Console.Write(">>");

                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);

                if(bValid)
                {
                    switch (select)
                    {
                        case 1:
                            Console.WriteLine("상태창");
                            player.ShowStatus();
                            break;

                        case 2:
                            Console.WriteLine("인벤토리");
                            player.ShowInventory(); 
                            break;
                        case 3:
                            Console.WriteLine("상점");
                            store.StartStroe(player);
                            break; 

                        case 4:
                            Console.WriteLine("던전");
                            dungeon.DungeonDescription(player);
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            bValid = false;
                            Thread.Sleep(500);
                            break; 
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(500);
                }
            }
        }


        void PlayerDie()
        {
            Console.Clear();
            Console.WriteLine(@"






 /$$     /$$ /$$$$$$  /$$   /$$       /$$$$$$$  /$$$$$$ /$$$$$$$$ /$$$$$$$            
|  $$   /$$//$$__  $$| $$  | $$      | $$__  $$|_  $$_/| $$_____/| $$__  $$           
 \  $$ /$$/| $$  \ $$| $$  | $$      | $$  \ $$  | $$  | $$      | $$  \ $$           
  \  $$$$/ | $$  | $$| $$  | $$      | $$  | $$  | $$  | $$$$$   | $$  | $$           
   \  $$/  | $$  | $$| $$  | $$      | $$  | $$  | $$  | $$__/   | $$  | $$           
    | $$   | $$  | $$| $$  | $$      | $$  | $$  | $$  | $$      | $$  | $$           
    | $$   |  $$$$$$/|  $$$$$$/      | $$$$$$$/ /$$$$$$| $$$$$$$$| $$$$$$$/ 
    |__/    \______/  \______/       |_______/ |______/|________/|_______/  
             


                          아무 키나 눌러서 부활...
");
            Console.ReadKey();
        }

        void PlayerVictory(string monsterName)
        {
            Console.Clear();
            Console.WriteLine(@"




 /$$    /$$ /$$$$$$  /$$$$$$  /$$$$$$$$ /$$$$$$  /$$$$$$$  /$$     /$$     /$$
| $$   | $$|_  $$_/ /$$__  $$|__  $$__//$$__  $$| $$__  $$|  $$   /$$/    | $$
| $$   | $$  | $$  | $$  \__/   | $$  | $$  \ $$| $$  \ $$ \  $$ /$$/     | $$
|  $$ / $$/  | $$  | $$         | $$  | $$  | $$| $$$$$$$/  \  $$$$/      | $$
 \  $$ $$/   | $$  | $$         | $$  | $$  | $$| $$__  $$   \  $$/       |__/
  \  $$$/    | $$  | $$    $$   | $$  | $$  | $$| $$  \ $$    | $$              
   \  $/    /$$$$$$|  $$$$$$/   | $$  |  $$$$$$/| $$  | $$    | $$         /$$
    \_/    |______/ \______/    |__/   \______/ |__/  |__/    |__/        |__/
                                                                                
                            {0}을 처치하였습니다 !  


                             아무 키나 눌러서 계속... 
                                                                                
",monsterName);
            Console.ReadKey();
        }


        public void Run()
        {
            GameStartScene();
            //CreateCharacter(); 
            //player.ShowInventory(); 
            //player.ShowStatus(); 
            //dungeon.DungeonDescription(player);
            //PlayerVictory("스켈레톤"); 
        }
     }
}
