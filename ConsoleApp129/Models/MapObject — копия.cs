using System;
using System.Xml.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    /// Базовый класс для всех объектов карты.
    /// </summary>
    [XmlInclude(typeof(Wall))]
    [XmlInclude(typeof(Field))]
    [XmlInclude(typeof(Tree))]
    [XmlInclude(typeof(Person))]
    [XmlInclude(typeof(Hero))]
    [XmlInclude(typeof(Enemy))]
    public abstract class MapObject
    {
        /// <summary>
        /// Абстрактный метод для отрисовки объекта на карте.
        /// </summary>
        /// <returns>Символ, представляющий объект.</returns>
        public abstract char Rendering_on_the_map();

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MapObject() { }
    }

    /// <summary>
    /// Представляет стену на карте.
    /// </summary>
    public class Wall : MapObject
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Wall() { }
        /// <summary>
        /// Возвращает символ для отображения стены на карте.
        /// </summary>
        /// <returns>Символ '+'.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }

    /// <summary>
    /// Представляет поле (пустую ячейку) на карте.
    /// </summary>
    public class Field : MapObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Field() { }
        /// <summary>
        /// Возвращает символ для отображения поля на карте.
        /// </summary>
        /// <returns>Символ '.'.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }

    /// <summary>
    /// Представляет дерево на карте.
    /// </summary>
    public class Tree : MapObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Tree() { }
        /// <summary>
        /// Возвращает символ для отображения дерева на карте.
        /// </summary>
        /// <returns>Символ 'T'.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'T';
        }
    }
}
