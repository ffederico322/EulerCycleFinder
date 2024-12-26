namespace EulerCycleFinder.Models
{
    // Класс для хранения входных данных, которые включают идентификатор графа, путь к файлу и время обработки
    public class InputData
    {
        // Идентификатор графа
        public int GraphId { get; }

        // Путь к файлу, содержащему данные для графа
        public string FilePath { get; }

        // Время, когда были получены данные или началась обработка
        public DateTime ProcessingTime { get; }

        // Конструктор для инициализации объекта InputData с заданными значениями
        public InputData(int graphId, string filePath)
        {
            // Устанавливаем идентификатор графа
            GraphId = graphId;

            // Устанавливаем путь к файлу
            FilePath = filePath;

            // Устанавливаем текущее время как время обработки
            ProcessingTime = DateTime.Now;
        }
    }
}