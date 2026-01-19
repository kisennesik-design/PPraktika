using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    /// Представляет игровую карту, содержащую различные объекты.
    /// </summary>
    [XmlRoot("Map")]
    public class Map
    {
        private const int MapWidth = 25;
        private const int MapHeight = 25;
        private const int WallSpawnChance = 5;
        private const int EnemySpawnChance = 1;
        private const int TreeSpawnChance = 4;

        [XmlIgnore]
        private Random _rand = new Random();

        /// <summary>
        /// Получает или задает данные карты в виде списка списков объектов карты.
        /// </summary>
        [XmlArray("MapData")]
        [XmlArrayItem("Row")]
        public List<List<MapObject>> MapData { get; set; }


        /// <summary>
        /// Получает или задает данные карты в виде списка списков объектов карты. Используется для игровой логики.
        /// </summary>
        public List<List<MapObject>> _map { get; set; }


        private Stack<Command> _commandHistory = new Stack<Command>();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Map"/>.
        /// </summary>
        public Map()
        {
            MapData = new List<List<MapObject>>();
            _map = new List<List<MapObject>>();
        }

        /// <summary>
        /// Генерирует карту, заполняя ее различными объектами.
        /// </summary>
        public void GenerateMap()
        {
            _map = new List<List<MapObject>>();

            for (int i = 0; i < MapWidth; i++)
            {
                List<MapObject> row = new List<MapObject>();
                for (int j = 0; j < MapHeight; j++)
                {
                    int randomValue = _rand.Next(100);
                    MapObject newObject = new Field();

                    if (randomValue > 1 && randomValue < WallSpawnChance + 1)
                    {
                        newObject = new Wall();
                    }
                    else if (randomValue < EnemySpawnChance + 1)
                    {
                        newObject = new Enemy(i, j);
                    }
                    else if (randomValue > WallSpawnChance && randomValue < WallSpawnChance + TreeSpawnChance + 1)
                    {
                        newObject = new Tree();
                    }
                    row.Add(newObject);
                }
                _map.Add(row);
            }
            PlaceHero();
            ConvertMapToList();
        }

        /// <summary>
        /// Конвертирует 2D массив карты в список списков.
        /// </summary>
        public void ConvertMapToList()
        {
            MapData = new List<List<MapObject>>();
            for (int i = 0; i < MapWidth; i++)
            {
                List<MapObject> row = new List<MapObject>();
                for (int j = 0; j < MapHeight; j++)
                {
                    row.Add(_map[i][j]);
                }
                MapData.Add(row);
            }
        }

        /// <summary>
        /// Конвертирует список списков объектов карты обратно в 2D массив.
        /// </summary>
        /// <exception cref="GameException">Возникает, если данные карты повреждены.</exception>
        public void ConvertListToMap()
        {
            _map = new List<List<MapObject>>();

            for (int i = 0; i < MapWidth; i++)
            {
                List<MapObject> row = new List<MapObject>();
                if (MapData == null || MapData.Count <= i || MapData[i] == null)
                {
                    throw new GameException($"Неверные данные карты: строка {i} отсутствует или равна null.");
                }
                for (int j = 0; j < MapHeight; j++)
                {
                    if (MapData[i].Count <= j || MapData[i][j] == null)
                    {
                        throw new GameException($"Неверные данные карты: столбец {j} в строке {i} отсутствует или равен null.");
                    }
                    row.Add(MapData[i][j]);
                }
                _map.Add(row);
            }
        }

        /// <summary>
        /// Размещает героя на карте.
        /// </summary>
        private void PlaceHero()
        {
            int heroX = MapWidth / 2;
            int heroY = MapHeight / 2;
            _map[heroX][heroY] = new Hero(heroX, heroY);
        }

        /// <summary>
        /// Отрисовывает карту в консоли.
        /// </summary>
        public void DrawMap()
        {
            Console.Clear();  // ← добавь эту строку в начало метода

            for (int i = 0; i < GetSize().Item1; i++)
            {
                for (int j = 0; j < GetSize().Item2; j++)
                {
                    Console.Write(_map[i][j].Rendering_on_the_map() + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Перемещает персонажей (в настоящее время только врагов) на карте.
        /// </summary>
        /// <param name="key">Клавиша, нажатая для управления героем (необязательно).</param>
        public void MovePersons(ConsoleKey? key = null)
        {
            List<List<MapObject>> newMap = new List<List<MapObject>>();
            for (int i = 0; i < MapWidth; i++)
            {
                List<MapObject> newRow = new List<MapObject>();
                for (int j = 0; j < MapHeight; j++)
                {
                    newRow.Add(null);
                }
                newMap.Add(newRow);
            }

            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    newMap[i][j] = _map[i][j];
                }
            }

            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    if (!key.HasValue && _map[i][j] is Enemy)
                    {
                        int direction = _rand.Next(4);
                        int newX = i, newY = j;
                        switch (direction)
                        {
                            case 0:
                                newX = (i - 1 + MapWidth) % MapWidth;
                                break;
                            case 1:
                                newX = (i + 1) % MapWidth;
                                break;
                            case 2:
                                newY = (j - 1 + MapHeight) % MapHeight;
                                break;
                            case 3:
                                newY = (j + 1) % MapHeight;
                                break;
                        }

                        if (newMap[newX][newY] is Field)
                        {
                            newMap[newX][newY] = _map[i][j];
                            newMap[i][j] = new Field();
                            ((Enemy)newMap[newX][newY]).pointX = newX;
                            ((Enemy)newMap[newX][newY]).pointY = newY;
                        }
                    }
                }
            }


            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    _map[i][j] = newMap[i][j];
                }
            }
            ConvertMapToList();
        }

        /// <summary>
        /// Получает объект карты по указанным координатам.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>Объект карты.</returns>
        /// <exception cref="GameException">Если координаты выходят за пределы.</exception>
        public MapObject GetMapObject(int x, int y)
        {
            if (x >= 0 && x < GetSize().Item1 && y >= 0 && y < GetSize().Item2)
            {
                return _map[x][y];
            }
            // Убрали throw — пусть MoveHero сам обрабатывает
            return null;  // или throw new ArgumentOutOfRangeException("Координаты вне карты");
        }

        /// <summary>
        /// Проверяет, является ли ячейка проходимой.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <returns>True, если ячейка проходима, иначе false.</returns>
        public bool IsWalkable(int x, int y)
        {
            MapObject obj = GetMapObject(x, y);
            return obj is Field;
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        public void ExecuteCommand(Command command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        /// <summary>
        /// Перемещает героя в указанную позицию.
        /// </summary>
        /// <param name="newX">Новая X-координата.</param>
        /// <param name="newY">Новая Y-координата.</param>
        /// <param name="hero">Герой.</param>
        public void MoveHero(int newX, int newY, Hero hero)
        {
            if (newX < 0 || newX >= MapWidth || newY < 0 || newY >= MapHeight)
            {
                throw new GameException($"Нельзя двигаться за пределы карты: координаты ({newX}, {newY}) недопустимы");
                // или без исключения:
                // Console.WriteLine("Нельзя туда идти!");
                // return;
            }

            if (_map[hero.pointX][hero.pointY] is Hero)
            {
                _map[hero.pointX][hero.pointY] = new Field();
            }
            hero.pointX = newX;
            hero.pointY = newY;
            _map[newX][newY] = hero;
        }

        /// <summary>
        /// Отменяет последнюю команду.
        /// </summary>
        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                Command lastCommand = _commandHistory.Pop();
                lastCommand.Undo();
            }
        }

        /// <summary>
        /// Возвращает размер карты как (Ширина, Высота).
        /// </summary>
        /// <returns>Размер карты.</returns>
        /// <exception cref="GameException">Если MapWidth или MapHeight меньше или равны нулю.</exception>
        public (int, int) GetSize()
        {
            if (MapWidth <= 0 || MapHeight <= 0)
            {
                throw new GameException("Размер карты должен быть больше нуля");
            }
            return (MapWidth, MapHeight);
        }
    }
}
