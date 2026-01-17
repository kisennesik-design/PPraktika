using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Представляет пользовательские исключения, специфичные для игры.
    /// </summary>
    public class GameException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GameException"/>.
        /// </summary>
        public GameException() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GameException"/> с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, объясняющее причину исключения.</param>
        public GameException(string message) : base(message) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GameException"/> с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое является причиной этого исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, объясняющее причину исключения.</param>
        /// <param name="innerException">Исключение, которое является причиной текущего исключения, или пустая ссылка, если внутреннее исключение не указано.</param>
        public GameException(string message, Exception innerException) : base(message, innerException) { }
    }
}
