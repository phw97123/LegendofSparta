using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendofSparta
{
    internal class SceneSetting
    {
        static void Main(string[] args)
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



            ");

            Console.WriteLine("                      아무 키나 눌러서 실행     ");

            Console.ReadKey();

            Console.Clear();
            Console.WriteLine(" 플레이어의 이름을 정해주세요.");
            Console.WriteLine("┌─────────────────────────────────┐");
            Console.WriteLine("│                                 │");
            Console.WriteLine("│    이름:                        │");
            Console.WriteLine("│                                 │");
            Console.WriteLine("└─────────────────────────────────┘");

            Console.SetCursorPosition(11, Console.CursorTop - 3);
            //─┐│└┘5
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("안녕하세요 {플레이어 네임 }님");
            Console.WriteLine("여기는 전설이 되기 위해 많은 모험가들이 모이는 마을입니다");
            Console.WriteLine("이곳에서 전설이 되기 위해 준비의 준비를 하세요");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");

            Console.WriteLine();
            Console.WriteLine("1.상태창");
            Console.WriteLine("2.인벤토리");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해주세요.");
            Console.Write(">>");

            

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────┐");
            Console.WriteLine("│                           0.닫기│");
            Console.WriteLine("│            [상태창]             │");
            Console.WriteLine("│                                 │");
            Console.WriteLine("│  Hwon             초보자        │");
            Console.WriteLine("│ =============================== │");
            Console.WriteLine("│  Lv. 01         HP 100/100      │");
            Console.WriteLine("│ =============================== │");
            Console.WriteLine("│  공격력 20                      │");
            Console.WriteLine("│  방어력 15                      │");
            Console.WriteLine("│   Gold  1000                    │");
            Console.WriteLine("│                                 │");
            Console.WriteLine("└─────────────────────────────────┘");

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────────────────┐");
            Console.WriteLine("│                                   0.닫기│");
            Console.WriteLine("│             [인벤토리]                  │");
            Console.WriteLine("│                                         │");
            Console.WriteLine("│ 1.무쇠갑옷 방어력 + 5                   │");
            Console.WriteLine("│   " + "\x1b[2;37m" + "무쇠로 만들어져 튼튼한 갑옷" + "\x1b[0m".PadRight(15) + "│"); // \x1b[2;37m: 회색
            Console.WriteLine("│ ======================================  │");
            Console.WriteLine("│ 2.낡은 검 공격력 +2                     │");
            Console.WriteLine("│   " + "\x1b[2;37m" + "쉽게 볼 수 있는 낡은 검" + "\x1b[0m".PadRight(19) + "│"); // \x1b[2;37m: 회색
            Console.WriteLine("│ ======================================  │");
            Console.WriteLine("│                                         │");
            Console.WriteLine("└─────────────────────────────────────────┘");


           

            //Console.OutputEncoding = System.Text.Encoding.UTF8; // UTF-8 인코딩 설정

            //string itemName = "무쇠로 만들어져 튼튼한 갑옷";

            //int borderLength = 50; // 테두리와 구분선의 총 길이
            //int itemNameLength = itemName.Length; // 아이템 이름 + 여백

            //int padding = borderLength; // 아이템과 여백의 차이

            //Console.WriteLine("┌" + new string('─', borderLength) + "┐");
            //Console.WriteLine("│ " + "\x1b[2;37m" + itemName.PadLeft(itemNameLength) + "\x1b[0m".PadRight(7) + "│"); // \x1b[2;37m: 회색
            //Console.WriteLine("└" + new string('─', borderLength) + "┘");

            Console.ReadKey();


            string dragon = @"--------------------------------------------------------------------------------
                  __====-_  _-====___
           _--^^^#####//      \\#####^^^--_
        _-^##########// (    ) \\##########^-_
       -############//  |\^^/|  \\############-
     _/############//   (@::@)   \\############\_
    /#############((     \\//     ))#############\
   -###############\\    (oo)    //###############-
  -#################\\  / "" "" \  //#################-
 -###################\\/  (\\/)  \//###################-
_#/|##########/\######(   (/^^\)   )######/\##########|\#_
 |/  |/  |/  |/  |/  |(  |/  \|  )|  |/  |/  |/  |/  |/  |/

--------------------------------------------------------------------------------";

            Console.WriteLine(dragon);

            string skle = @"      .-.
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
            Console.Write(skle);

        }
    }
}
