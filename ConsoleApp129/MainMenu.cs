using System;

namespace ConsoleApp129
{
    public class MainMenu
    {
        public void ShowMenu(Map gameMap)
        {
            string[] menuItems = { "Начать игру", "Выход" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"> {menuItems[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuItems[i]}");
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuItems.Length;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedIndex == 0)
                        {
                            StartGame(gameMap);
                            return;
                        }
                        else if (selectedIndex == 1)
                        {
                            Console.WriteLine("Выход из игры...");
                            Environment.Exit(0);
                        }
                        break;
                }
            }
        }

        private void StartGame(Map gameMap)
        {
            gameMap.GenerateMap();
            while (true)
            {
                Console.Clear();
                gameMap.DrawMap();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                gameMap.MovePersons(keyInfo.Key);
                gameMap.MovePersons();

                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
