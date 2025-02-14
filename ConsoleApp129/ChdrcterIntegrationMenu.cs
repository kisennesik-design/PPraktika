using System;

namespace ConsoleApp129
{
    public class CharacterInteractionMenu
    {
        private ConsoleColor textColor = ConsoleColor.DarkRed;
        public void ShowInteractionMenu(Hero hero, Person person)
        {
            string[] menuItems = { "Поговорить (не реализовано)", "Атаковать (не реализовано)", "Назад" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Взаимодействие с {person.Rendering_on_the_map()}");

                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"> {menuItems[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine($"  {menuItems[i]}");
                        Console.ResetColor();
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
                            Console.ForegroundColor = textColor;
                            Console.WriteLine("Попытка поговорить... (не реализовано)");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 1)
                        {
                            Console.ForegroundColor = textColor;
                            Console.WriteLine("Попытка атаковать... (не реализовано)");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 2)
                        {
                            return;
                        }
                        break;
                }
            }
        }
    }
}
