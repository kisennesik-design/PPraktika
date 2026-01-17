using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Главный класс программы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются).</param>
        static void Main(string[] args)
        {
            Map gameMap = new Map();
            MainMenu mainMenu = new MainMenu();

            try
            {
                mainMenu.ShowMenu(gameMap);
            }
            catch (GameException ex)
            {
                Console.WriteLine($"Игровая ошибка: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}
