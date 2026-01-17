using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Предоставляет главное меню для игры.
    /// </summary>
    public class MainMenu
    {
        private bool _inGame = false;
        private SaveLoadManager _saveLoadManager = new SaveLoadManager();
        private Map _gameMap;

        /// <summary>
        /// Отображает главное меню и обрабатывает ввод пользователя.
        /// </summary>
        /// <param name="gameMap">Начальная карта игры.</param>
        /// <exception cref="GameException">Возникает, если <paramref name="gameMap"/> находится в недопустимом состоянии.</exception>
        public void ShowMenu(Map gameMap)
        {
            _gameMap = gameMap;
            string[] menuItems = { "Начать игру", "Загрузить игру", "Сохранить игру", "Выход" };
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
                        switch (selectedIndex)
                        {
                            case 0:
                                _inGame = true;
                                StartGame(_gameMap);
                                _inGame = false;
                                return;
                            case 1:
                                try
                                {
                                    if (_saveLoadManager.SaveExists())
                                    {
                                        _gameMap = _saveLoadManager.LoadGame();
                                        Console.WriteLine("Игра загружена!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Нет сохраненной игры для загрузки.");
                                    }
                                }
                                catch (GameException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.ReadKey();
                                break;
                            case 2:
                                try
                                {
                                    _saveLoadManager.SaveGame(_gameMap);
                                    Console.WriteLine("Игра сохранена!");
                                }
                                catch (GameException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.ReadKey();
                                break;
                            case 3:
                                Console.WriteLine("Выход из игры...");
                                Environment.Exit(0);
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Запускает игровой цикл.
        /// </summary>
        /// <param name="gameMap">Игровая карта.</param>
        /// <exception cref="GameException">Возникает, если игровая карта равна null.</exception>
        private void StartGame(Map gameMap)
        {
            if (gameMap.MapData == null || gameMap.MapData.Count == 0 || gameMap.MapData[0] == null)
            {
                gameMap.GenerateMap();
            }

            if (gameMap == null)
            {
                throw new GameException("Игровая карта равна null.");
            }

            gameMap._map = gameMap.MapData;
            _inGame = true;

            while (_inGame)
            {
                Console.Clear();
                gameMap.DrawMap();

                Console.WriteLine("Нажмите M, чтобы открыть меню, Escape для выхода, U для отмены последнего действия.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    _inGame = false;
                    return;
                }
                else if (keyInfo.Key == ConsoleKey.M)
                {
                    ShowInGameMenu(gameMap);
                }
                else if (keyInfo.Key == ConsoleKey.U)
                {
                    gameMap.UndoLastCommand();
                }
                else
                {
                    Hero hero = FindHero(gameMap);
                    if (hero != null)
                    {
                        MoveCommand moveCommand = new MoveCommand(gameMap, hero, keyInfo.Key);
                        gameMap.ExecuteCommand(moveCommand);
                    }
                    gameMap.MovePersons();
                }

                System.Threading.Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Находит героя на игровой карте.
        /// </summary>
        /// <param name="gameMap">Игровая карта.</param>
        /// <returns>Объект героя или null, если герой не найден.</returns>
        private Hero FindHero(Map gameMap)
        {
            for (int i = 0; i < gameMap.GetSize().Item1; i++)
            {
                for (int j = 0; j < gameMap.GetSize().Item2; j++)
                {
                    if (gameMap._map[i][j] is Hero)
                    {
                        return (Hero)gameMap._map[i][j];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Отображает внутриигровое меню.
        /// </summary>
        /// <param name="gameMap">Текущая игровая карта.</param>
        private void ShowInGameMenu(Map gameMap)
        {
            string[] menuItems = { "Продолжить игру", "Сохранить игру", "Загрузить игру", "Выход в главное меню" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Внутриигровое меню ===");

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
                        switch (selectedIndex)
                        {
                            case 0:
                                return;
                            case 1:
                                try
                                {
                                    _saveLoadManager.SaveGame(gameMap);
                                    Console.WriteLine("Игра сохранена!");
                                }
                                catch (GameException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.ReadKey();
                                break;
                            case 2:
                                try
                                {
                                    if (_saveLoadManager.SaveExists())
                                    {
                                        _gameMap = _saveLoadManager.LoadGame();
                                        Console.WriteLine("Игра загружена!");
                                        Console.ReadKey();
                                        StartGame(_gameMap);
                                        _inGame = false;
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Нет сохраненной игры для загрузки!");
                                        Console.ReadKey();
                                    }
                                }
                                catch (GameException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.ReadKey();
                                }
                                break;
                            case 3:
                                _inGame = false;
                                return;
                        }
                        break;
                }
            }
        }
    }
}
