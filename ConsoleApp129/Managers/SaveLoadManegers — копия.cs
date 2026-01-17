using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp129
{
    /// <summary>
    /// Управляет сохранением и загрузкой состояния игры.
    /// </summary>
    public class SaveLoadManager
    {
        private readonly string _saveFilePath = "savegame.xml";

        /// <summary>
        /// Сохраняет текущее состояние игры в XML-файл.
        /// </summary>
        /// <param name="gameMap">Карта игры для сохранения.</param>
        /// <exception cref="GameException">Выбрасывается при ошибках записи в файл.</exception>
        public void SaveGame(Map gameMap)
        {
            try
            {
                gameMap.ConvertMapToList();

                XmlSerializer serializer = new XmlSerializer(typeof(Map));

                using (FileStream fileStream = new FileStream(_saveFilePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, gameMap);
                }
            }
            catch (IOException ex)
            {
                throw new GameException("Ошибка при записи файла сохранения.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new GameException("Нет доступа для записи файла сохранения.", ex);
            }
            catch (Exception ex)
            {
                throw new GameException($"Ошибка при сохранении игры: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Загружает состояние игры из XML-файла.
        /// </summary>
        /// <returns>Загруженная карта игры.</returns>
        /// <exception cref="GameException">Выбрасывается, если файл сохранения не найден или поврежден.</exception>
        public Map LoadGame()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Map));

                using (FileStream fileStream = new FileStream(_saveFilePath, FileMode.Open))
                {
                    Map loadedMap = (Map)serializer.Deserialize(fileStream);
                    loadedMap.ConvertListToMap();
                    return loadedMap;
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new GameException("Файл сохранения не найден.", ex);
            }
            catch (IOException ex)
            {
                throw new GameException("Ошибка при чтении файла сохранения.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new GameException("Файл сохранения поврежден или имеет неверный формат.", ex);
            }
            catch (Exception ex)
            {
                throw new GameException($"Ошибка при загрузке игры: {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Проверяет, существует ли файл сохранения.
        /// </summary>
        /// <returns><see langword="true"/>, если файл сохранения существует; в противном случае — <see langword="false"/>.</returns>
        public bool SaveExists()
        {
            return File.Exists(_saveFilePath);
        }
    }
}

