using LegendofSparta.GameManger;
using System.Runtime.CompilerServices;

namespace LegendofSparta  
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Window11에서 흔히 일어나는 일로 설정 -> 개인 정보 보호 및 보안-> 개발자용 -> 터미널에서 콘솔을 기본 터미널로 설정하면 된다 
             */
            Console.SetWindowSize(80, 30);

            Console.CursorVisible = false;
            // Console.Clear(); 
            GameManager game = new GameManager();
            game.Run();

            

        }
    }
}