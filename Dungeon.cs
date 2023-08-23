using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendofSparta.PlayerClass;
using LegendofSparta.MonsterClass;
 

namespace LegendofSparta
{
    internal class Dungeon
    {
         Monster skeleton;
         Monster dragon;

        public bool bEscape = false; 

        //한글자씩 출력해주는 함수
         void TextOutput(string text)
         {
            for (int i = 0; i < text.Length; i++)
            {
                string text1 = text.Substring(i, 1);
                Console.Write(text1);
                Thread.Sleep(80);
            }
            Console.WriteLine();
         }

        //0 또는 1을 반환하여 50% 확률로 도망 
        bool TryEscape(string monsterName) 
         {
            Random random = new Random();
            if (monsterName == "스켈레톤")
                return random.Next(2) == 0;
            else
                return random.Next(100) < 30; 
          
         }
        //몬스터 생성
        public Dungeon() 
        {
            skeleton = new Monster();
            skeleton.MonsterStatus.Name = "스켈레톤";
            skeleton.MonsterStatus.Hp = 200; 
            skeleton.MonsterStatus.MaxHp = 200;
            skeleton.MonsterStatus.Atk = 40;
            skeleton.MonsterStatus.Def = 15;
            skeleton.RewardGold = 1000;
            skeleton.RewardExp = 100; 
            skeleton.Image = @"               .-.
              (o.o)
               |=|
              __|__
            //.=|=.\\
           // .=|=. \\
           \\ .=|=. //
            \\(_=_)//
             (:| |:)
              || ||
              () ()
";
            
            dragon = new Monster();
            dragon.MonsterStatus.Name = "드래곤";
            dragon.MonsterStatus.Hp = 300;
            dragon.MonsterStatus.MaxHp = 300;
            dragon.MonsterStatus.Atk = 40;
            dragon.MonsterStatus.Def = 50;
            dragon.RewardGold = 3000;
            dragon.RewardExp = 300; 

            //dragon.Image = @"                      __====-_  _-====___
            //                _--^^^#####//      \\#####^^^--_
            //             _-^##########// (    ) \\##########^-_
            //            -############//  |\^^/|  \\############-
            //          _/############//   (@::@)   \\############\_
            //         /#############((     \\//     ))#############\
            //        -###############\\    (oo)    //###############-
            //      -#################\\  / "" "" \  //#################-
            //     -###################\\/  (\\/)  \//###################-
            //    _#/|##########/\######(   (/^^\)   )######/\##########|\#_
            //     |/  |/  |/  |/  |/  |(  |/  \|  )|  |/  |/  |/  |/  |/  |/
            //";

            dragon.Image = @"               __====-_  _-====___
            _--^^^#//       \\#^^^--_ 
         _-^######//  (   )  \\######^-_
        -########//  |\\^^/|  \\########-
      _/########//   (@::@)    \\########\_
     /#########((     \\//      ))#########\
    -###########\\    (oo)     //###########-
   -############\\  / "" "" \\  //#############-
  -##############\\/ (\\/) \\//###############-
_#/|#####/\\######(  (/^^\\) )######/\\######|\\#_
|/  |/  |/   |/  | ( |/  \| )|  |/  |/  |/  |/";




        }

        //던전 설명 및 입장 선택
        public void DungeonDescription(Player player)
        {
            while(true)
            {
                Console.Clear();

                if (player.PlayerStatus.Hp <= 0) //플레이어가 죽었으면
                    break;
                else if (player.bVictory == true) //플레이어가 이겼다면
                    break;
                else if (bEscape == true) //도망에 성공하였다면
                {
                    bEscape = false;
                    break;
                }
                   

                Console.WriteLine();
                string str = @"                               [던전]

             공격력의 수치에 따라 랜덤으로 공격을 합니다          
         방어력의 수치에 따라 실제 입는 공격력은 조절 됩니다         
                     어디로 가시겠습니까?         ";
                Console.WriteLine();
                Console.WriteLine(str);
                Console.WriteLine("              0.나가기  1.스켈레톤의 방  2.드래곤의방");
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
                        ShowDungeon(skeleton, player);

                    }
                    else if (select == 2)
                    {
                        ShowDungeon(dragon, player);

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

        //몬스터가 있는 실제 던전 진입
        public void ShowDungeon(Monster monster,Player player)
        {
            //몬스터 체력 회복
            monster.MonsterStatus.Hp = monster.MonsterStatus.MaxHp;

            //입장할 때 플레이어의 체력
            int playerEnterHp = player.PlayerStatus.Hp;

            while(true)
            {
                string battleDescription = "";

                //플레이어가 죽으면 체력이 음수로 떨어지지 않게
                player.PlayerStatus.Hp = player.PlayerStatus.Hp <= 0 ? 0 : player.PlayerStatus.Hp;

                //몬스터가 죽으면 체력이 음수로 떨어지지 않게 
                monster.MonsterStatus.Hp = monster.MonsterStatus.Hp<= 0 ? 0 : monster.MonsterStatus.Hp;

                Console.Clear();
                Console.WriteLine("  ──────────────────────────────────────────────────────────────────────");
                Console.WriteLine($"   {monster.ShowMonsterState()}");
                Console.WriteLine();
                Console.WriteLine(monster.Image);
                Console.WriteLine("  ──────────────────────────────────────────────────────────────────────");
                Console.WriteLine("                                │");
                Console.WriteLine("                                │");
                Console.WriteLine("                                │");
                Console.WriteLine("                                │");

                Console.SetCursorPosition(3, 16);
                Console.WriteLine($"{player.PlayerStatus.Name}  {player.PlayerStatus.Level}");
                Console.WriteLine($"   HP {player.PlayerStatus.Hp}/{player.PlayerStatus.MaxHp}  MP {player.PlayerStatus.Mp}/{player.PlayerStatus.MaxMp}");
                Console.WriteLine($"   Atk {player.PlayerStatus.Atk}");
                Console.WriteLine($"   Def {player.PlayerStatus.Def}");
                Console.SetCursorPosition(35, 16);
                Console.WriteLine("1.공격");
                Console.SetCursorPosition(35, 17);
                Console.WriteLine("2.치명타 공격");
                Console.SetCursorPosition(35, 18);
                Console.WriteLine("3.도망");
                Console.WriteLine();
                Console.Write(" >> ");

                if (player.PlayerStatus.Hp <= 0) //플레이어가 죽었을 때 
                {
                    Thread.Sleep(500);
                    break;
                }
                else if(monster.MonsterStatus.Hp<= 0) //몬스터가 죽었을 때 보상 
                {
                    Thread.Sleep(1000);

                    Console.Clear(); 
                    player.bVictory = true;
                    player.victoryMonster = monster.MonsterStatus.Name; 

                    //플레이어의 남은 체력의 비율 계산으로 골드 획득
                    double playerHealthRatio = (double)playerEnterHp / player.PlayerStatus.Hp;

                    //최대 감소 골드 
                    int maxDecreaseGold= 100;
                   
                    //골드 보상 게산
                    int rewardGold = monster.RewardGold - (int)(maxDecreaseGold * playerHealthRatio);

                    player.PlayerStatus.Gold += rewardGold;
                    player.PlayerStatus.Exp += monster.RewardExp;

                    Console.WriteLine($" {rewardGold}G 을 얻었습니다");
                    Console.WriteLine($" {monster.RewardExp}의 경험치를 얻었습니다");
                    player.LevelUpWithExp(); 
                    Thread.Sleep(2000);
                    break;
                }
                
               

                string? answer = Console.ReadLine();
                int select;
                bool bValid = int.TryParse(answer, out select);

                if (bValid)
                {

                    if (select == 1)
                    {
                        //플레이어 공격 데미지
                        int monsterDamage = monster.TakeDamage(player.PlayerStatus.Def);

                        //몬스터의 공격 데미지
                        int playerDamage = player.TakeDamage(monster.MonsterStatus.Def);

                        //전투 설명 출력
                        //플레이어의 선공
                        Console.SetCursorPosition(48, 4);
                        battleDescription = $"{player.PlayerStatus.Name} 의 공격!";
                        TextOutput(battleDescription);

                        Console.SetCursorPosition(48, 5);
                        battleDescription = $"{monster.MonsterStatus.Name} 은 {playerDamage}의 피해를 입었다.";
                        TextOutput(battleDescription);
                        Console.WriteLine();

                        monster.MonsterStatus.Hp -= playerDamage;

                        //플레이어의 선공으로 몬스터가 죽으면 종료 
                        if(monster.MonsterStatus.Hp > 0)
                        {
                            Console.SetCursorPosition(48, 7);
                            battleDescription = $"{monster.MonsterStatus.Name}의 공격!";
                            TextOutput(battleDescription);
                            Console.WriteLine();

                            Console.SetCursorPosition(48, 8);
                            battleDescription = $"{player.PlayerStatus.Name} 은 {monsterDamage}의 피해를 입었다.";
                            TextOutput(battleDescription);
                            Console.WriteLine();

                            player.PlayerStatus.Hp -= monsterDamage;

                            Thread.Sleep(1200);
                        }
                    }
                    else if (select == 2) //치명타 공격
                    {
                        //치명타 공격은 10의 마나를 소모하여 마나가 있는지 확인
                        if(player.PlayerStatus.Mp>=10)
                        {
                            int monsterDamage = monster.TakeDamage(player.PlayerStatus.Def);
                            int playerDamage = player.CriticalTakeDamage(monster.MonsterStatus.Def);

                            Console.SetCursorPosition(48, 4);
                            battleDescription = $"{player.PlayerStatus.Name} 의 치명타 공격!";
                            TextOutput(battleDescription);

                            Console.SetCursorPosition(48, 5);
                            battleDescription = $"{monster.MonsterStatus.Name} 은 {playerDamage}의 피해를 입었다.";
                            TextOutput(battleDescription);
                            Console.WriteLine();

                            monster.MonsterStatus.Hp -= playerDamage;

                            if (monster.MonsterStatus.Hp > 0)
                            {
                                Console.SetCursorPosition(48, 7);
                                battleDescription = $"{monster.MonsterStatus.Name}의 공격!";
                                TextOutput(battleDescription);
                                Console.WriteLine();

                                Console.SetCursorPosition(48, 8);
                                battleDescription = $"{player.PlayerStatus.Name} 은 {monsterDamage}의 피해를 입었다.";
                                TextOutput(battleDescription);
                                Console.WriteLine();

                                player.PlayerStatus.Hp -= monsterDamage;

                                Thread.Sleep(1500);
                            }
                        }
                        else
                        {
                            Console.WriteLine("플레이어의 Mp가 부족합니다.");
                            Thread.Sleep(500);
                        }
                        
                    }
                    else if (select == 3)
                    {
                        if(TryEscape(monster.MonsterStatus.Name))
                        {
                            Console.WriteLine("성공적으로 도망쳤습니다.");
                            Thread.Sleep(500); 
                            bEscape = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("도망에 실패하였습니다.");
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
    }
}
