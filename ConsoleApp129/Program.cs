using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    class Program
    {
        static void Main(string[] args)
        {
            Map gameMap = new Map();
            gameMap.Map_generation();


            while (true)
            {
                Console.Clear();
                gameMap.Drawing_the_map();


                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }


                gameMap.MovePersons(keyInfo.Key);


                gameMap.MovePersons();

                Thread.Sleep(200);
            }
        }
    }
}
