using System;
using System.Xml.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    /// Базовый класс для всех персонажей на карте.
    /// </summary>
    [XmlInclude(typeof(Hero))]
    [XmlInclude(typeof(Enemy))]
    public class Person : MapObject
    {
        /// <summary>
        /// Получает или задает координату X персонажа на карте.
        /// </summary>
        public int pointX { get; set; }

        /// <summary>
        /// Получает или задает координату Y персонажа на карте.
        /// </summary>
        public int pointY { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Person(int x, int y)
        {
            pointX = x;
            pointY = y;
        }

        /// <summary>
        /// Возвращает символ для отображения персонажа на карте.
        /// </summary>
        /// <returns>Символ '!'.</returns>
        public override char Rendering_on_the_map()
        {
            return '!';
        }
    }

    /// <summary>
    /// Представляет героя на карте.
    /// </summary>
    public class Hero : Person
    {
        /// <summary>
        /// Урон героя (можно менять в игре).
        /// </summary>
        public int Damage { get; set; } = 10;  // ← вот здесь добавлено поле

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Hero() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Hero(int x, int y) : base(x, y) { }

        /// <summary>
        /// Выполняет взаимодействие героя с объектом на текущей позиции.
        /// </summary>
        /// <param name="map">Текущая карта.</param>
        public void InteractWithObject(Map map)
        {
            MapObject obj = map.GetMapObject(pointX, pointY);

            if (obj is Enemy enemy)
            {
                Console.WriteLine($"Атака на врага! Урон: {Damage}");
                // Здесь можно добавить логику урона врагу
            }
            else if (obj is Tree)
            {
                Console.WriteLine("Вы собрали дерево (ресурс или декор)");
            }
            else if (obj is Field)
            {
                Console.WriteLine("Здесь ничего нет...");
            }
            else
            {
                Console.WriteLine("Неизвестный объект");
            }
        }

        /// <summary>
        /// Возвращает символ для отображения героя на карте.
        /// </summary>
        /// <returns>Символ '@'.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '@';
        }
    }

    /// <summary>
    /// Представляет врага на карте.
    /// </summary>
    public class Enemy : Person
    {
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Enemy() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Enemy(int x, int y) : base(x, y) { }

        /// <summary>
        /// Возвращает символ для отображения врага на карте.
        /// </summary>
        /// <returns>Символ '!'.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            return base.Rendering_on_the_map();
        }
    }
}