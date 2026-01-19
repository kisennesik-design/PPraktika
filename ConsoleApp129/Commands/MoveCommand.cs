using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Абстрактный базовый класс для всех команд.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Выполняет команду.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Отменяет команду (если применимо).
        /// </summary>
        public abstract void Undo();
    }

    /// <summary>
    /// Команда для перемещения героя.
    /// </summary>
    public class MoveCommand : Command
    {
        private Map _map;
        private Hero _hero;
        private ConsoleKey _direction;
        private int _prevX, _prevY; // For Undo

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MoveCommand"/>.
        /// </summary>
        /// <param name="map">Карта.</param>
        /// <param name="hero">Герой.</param>
        /// <param name="direction">Направление перемещения (клавиша).</param>
        public MoveCommand(Map map, Hero hero, ConsoleKey direction)
        {
            _map = map;
            _hero = hero;
            _direction = direction;
        }

        /// <summary>
        /// Выполняет команду перемещения.
        /// </summary>
        public override void Execute()
        {
            _prevX = _hero.pointX;
            _prevY = _hero.pointY;

            int newX = _hero.pointX;
            int newY = _hero.pointY;

            switch (_direction)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    newX = _hero.pointX - 1;  
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    newX = _hero.pointX + 1;  
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    newY = _hero.pointY - 1;  
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    newY = _hero.pointY + 1;  
                    break;
            }

            try
            {
                if (_map.IsWalkable(newX, newY))
                {
                    _map.MoveHero(newX, newY, _hero);
                }
                else
                {
                    Console.WriteLine("Непроходимо!");
                }
            }
            catch (GameException ex)
            {
                Console.WriteLine(ex.Message);  // ← здесь будет "Нельзя двигаться за пределы карты..."
                Console.ReadKey(true);  // пауза, чтобы прочитать
            }
        }

        /// <summary>
        /// Отменяет команду перемещения.
        /// </summary>
        public override void Undo()
        {
            _map.MoveHero(_prevX, _prevY, _hero);
        }
    }
}