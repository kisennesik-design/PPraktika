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
        /// Инициализирует новый экземпляр класса <see cref="Person"/> с указанными координатами.
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
        /// Конструктор по умолчанию.
        /// </summary>
        public Hero() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Hero"/> с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Hero(int x, int y) : base(x, y)
        {
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
        /// Инициализирует новый экземпляр класса <see cref="Enemy"/> с указанными координатами.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        public Enemy(int x, int y) : base(x, y)
        {
        }

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
