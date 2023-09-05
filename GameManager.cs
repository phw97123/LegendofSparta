using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using LegendofSparta.MonsterClass;
using LegendofSparta.PlayerClass;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace LegendofSparta.GameManger
{
    
    internal class GameManager
    {
        Player player = new Player();
        Store store = new Store();
        Dungeon dungeon = new Dungeon();

        //직렬화
        //저장하기
        public void SaveGameData()
        {
            //파일 이름 
            string fileName = "_playerData.json";

            //디렉터리 경로
            string dataPath = "D:\\C#\\LegendofSparta\\LegendofSparta\\bin\\Debug\\net6.0";

          
            //파일이름, 디렉터리 경로를 나타내는 두 개 이상의 문자열을 하나의 경로로 결합
            string filePath = Path.Combine(dataPath, fileName);

            //Json 형식의 문자열로 변환 
            //Fomatting.Indented : 들여쓰기 하거나 사람이 읽기 쉬운 형식으로 지정하는데 사용 
            string playerJson = JsonConvert.SerializeObject(player, Formatting.Indented);

            //경로에 텍스트 저장 
            File.WriteAllText(filePath, playerJson);
        }
        
        //역직렬화
        //불러오기
        public void LoadGameData()
        {
            string fileName = "_playerData.json";
            string dataPath = "D:\\C#\\LegendofSparta\\LegendofSparta\\bin\\Debug\\net6.0";
           
            string filePath = Path.Combine(dataPath, fileName);

            //데이터 로드
            //파일이 실제로 존재하는가? 
            if (File.Exists(filePath))
            {
                string playerJson = File.ReadAllText(filePath);
                player = JsonConvert.DeserializeObject<Player>(playerJson); 
            }
            else
            {
                Console.WriteLine("데이터가 없습니다.");
                Thread.Sleep(1500);
            }
        }

        //글자 한개씩 출력
        public void TextOutput(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string text1 = text.Substring(i, 1);
                Console.Write(text1);
                Thread.Sleep(70);
            }
            Console.WriteLine();
        }

        //게임 타이틀 화면 
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
            

                          1.게임시작
                          2.불러오기

            ");

            Console.WriteLine("                        ");
            Console.Write(">> "); 

            string? answer = Console.ReadLine();
            int select;
            bool bValid = int.TryParse(answer, out select);

            if (bValid)
            {
                switch (select)
                {
                    case 1:
                        player = new Player(); 
                        CreateCharacter();
                        break;
                    case 2:
                        LoadGameData();
                        Console.Clear();
                        Thread.Sleep(500); 
                        Console.WriteLine("게임 불러오기 완료");
                        VillageScene(); 
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        break; 
                }
            }
        }

        //캐릭터 생성 화면
        void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine(); 
            Console.WriteLine("  플레이어의 이름을 정해주세요.");
            Console.WriteLine(" ┌─────────────────────────────────┐");
            Console.WriteLine(" │                                 │");
            Console.WriteLine(" │    이름:                        │");
            Console.WriteLine(" │                                 │");
            Console.WriteLine(" └─────────────────────────────────┘");

            Console.SetCursorPosition(11, Console.CursorTop - 3);

            player.PlayerStatus.Name = Console.ReadLine();

            CheckCharacterCreation();
        }

        //캐릭터 생성 확인 화면
        void CheckCharacterCreation()
        {
            int totalWidth = 34; // 전체 문자열 너비 
            int padding = (totalWidth - player.PlayerStatus.Name.Length - 2) / 2; // 가운데 정렬을 위한 패딩 계산 (|가 2개 있으니 -2)

            while (true)
            {
                Console.Clear();

                Console.WriteLine(); 
                Console.WriteLine("  이 이름으로 하시겠습니까?");
                Console.WriteLine(" ┌─────────────────────────────────┐");
                Console.WriteLine(" │                                 │");
                Console.WriteLine($" │{player.PlayerStatus.Name.PadLeft(padding + player.PlayerStatus.Name.Length).PadRight(totalWidth - 1)}│");
                //-1을 한 이유는 마지막 '|' 문자를 포함하여 전체 길이에 맞게 패딩을 적용
                Console.WriteLine(" │                                 │");
                Console.WriteLine(" │             Y / N               │");
                Console.WriteLine(" │                                 │");
                Console.WriteLine(" └─────────────────────────────────┘");
                Console.Write(">> ");

                string? answer = Console.ReadLine();
                if (answer == "y" || answer == "y")
                {
                    if(answer =="")
                    {
                        Console.WriteLine("이름이 없습니다 이름을 입력해주세요.");
                        Thread.Sleep(500);
                        break; 
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        TextOutput(" 캐릭터 생성 중...");
                        Thread.Sleep(500);
                        TextOutput(" 캐릭터 생성 완료 !");
                        Thread.Sleep(500);
                        TextOutput(" 마을로 들어가는 중...");
                        Thread.Sleep(1000);
                        VillageScene();
                        break;
                    }
                   
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

        //마을 입장 (메인 메뉴)
        void VillageScene()
        {
            while (true)
            {
                if (player.PlayerStatus.Hp <= 0) //플레이어가 죽으면 
                {
                    PlayerDie();
                    player.PlayerStatus.Hp = (int)(player.PlayerStatus.MaxHp * 0.1);
                }
                else if (player.bVictory == true) //몬스터 처치 시
                {
                    PlayerVictory(player.victoryMonster);
                    player.bVictory = false;
                    player.victoryMonster = "";
                }

                Console.Clear();
                Console.WriteLine(); 
                Console.WriteLine($"   안녕하세요 {player.PlayerStatus.Name}님");
                Console.WriteLine("   여기는 전설이 되기 위해 많은 모험가들이 모이는 마을입니다");
                Console.WriteLine("   이곳에서 전설이 되기 위해 준비를 하고 몬스터를 소탕하세요");
                Console.WriteLine();
                Console.WriteLine("  ----------------------------------------------------------");

                Console.WriteLine();
                Console.WriteLine("  1.상태창");
                Console.WriteLine("  2.인벤토리");
                Console.WriteLine("  3.상점");
                Console.WriteLine("  4.던전");
                Console.WriteLine("  5.회복센터");
                Console.WriteLine("  6.게임저장");
                Console.WriteLine("  0.게임종료");

                Console.WriteLine();
                Console.Write(">>");

                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);

                if (bValid)
                {
                    switch (select)
                    {
                        case 1:
                           // Console.WriteLine("상태창");
                            player.ShowStatus();
                            break;

                        case 2:
                           // Console.WriteLine("인벤토리");
                            player.ShowInventory();
                            break;
                        case 3:
                           // Console.WriteLine("상점");
                            store.StartStroe(player);
                            break;

                        case 4:
                           // Console.WriteLine("던전");
                            dungeon.DungeonDescription(player);
                            break;

                        case 5:
                           // Console.WriteLine("회복센터");
                            ShowRest(); 
                            break;
                        case 6:
                            SaveGameData();
                            Console.WriteLine("게임 저장 완료");
                            bValid = false;
                            Thread.Sleep(500);
                            break; 
                        case 0: //게임 종료
                            Environment.Exit(0);
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

        //플레이어가 죽으면 나오는 화면 
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

        //몬스터 처치시 나오는 화면
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
                                                                                
", monsterName);
            Console.ReadKey();
        }

        //회복센터 입장
        void ShowRest() 
        {
            while(true)
            {
                Console.Clear();
                string str = @"     




                               [회복센터]

                  300G 로 지친 몸과 마음을 회복하고 가세요 
                        
                         0.나가기  1.휴식하기
";

                Console.WriteLine(str);

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
                    else if (select == 1)
                    {  
                        //플레이어의 회복이 필요없다면
                       if(player.PlayerStatus.Hp == player.PlayerStatus.MaxHp && player.PlayerStatus.Mp == player.PlayerStatus.MaxMp) 
                       {
                            Console.WriteLine("엄살이네요");
                            Console.WriteLine("휴식할 필요가 없습니다.");
                            Thread.Sleep(800);
                            break; 
                       }
                       else if(player.PlayerStatus.Gold <300) //돈이 부족하다면
                       {
                            Console.WriteLine("돈이 없다면 회복할 수 없습니다");
                            Thread.Sleep(800);
                            break; 
                        }
                       else
                       {
                            //회복 중 
                           for(int i = 0; i<3; i++)
                           {
                                Console.Clear();
                                Console.WriteLine(); 
                                TextOutput(" 회복중...");
                                Console.WriteLine(" -100G");
                                Thread.Sleep(500);
                           }

                           //회복 완료 
                            player.PlayerStatus.Hp = player.PlayerStatus.MaxHp; 
                            player.PlayerStatus.Mp = player.PlayerStatus.MaxMp;

                            player.PlayerStatus.Gold -= 300; 

                            //상태창 보여주기
                            player.ShowStatus();
                            break;
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

        //게임 시작
        public void Run()
        {
             GameStartScene();
            
        }
    }
}
