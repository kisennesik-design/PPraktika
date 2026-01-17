using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Предоставляет меню для взаимодействия с персонажем в игре.
    /// </summary>
    public class CharacterInteractionMenu
    {
        private ConsoleColor _textColor = ConsoleColor.DarkRed;

        /// <summary>
        /// Отображает меню взаимодействия для данного героя и персонажа.
        /// </summary>
        /// <param name="hero">Персонаж героя.</param>
        /// <param name="person">Персонаж, с которым нужно взаимодействовать.</param>
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
                        Console.ForegroundColor = _textColor;
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

                        switch (selectedIndex)
                        {
                            case 0:
                                Console.ForegroundColor = _textColor;
                                Console.WriteLine("Попытка поговорить... (не реализовано)");
                                Console.ResetColor();
                                Console.ReadKey(true);
                                break;
                            case 1:
                                Console.ForegroundColor = _textColor;
                                Console.WriteLine("Попытка атаковать... (не реализовано)");
                                Console.ResetColor();
                                Console.ReadKey(true);
                                break;
                            case 2:
                                return;
                        }
                        break;
                }
            }
        }
    }
}
