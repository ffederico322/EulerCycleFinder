using System.Text;

namespace EulerCycleFinder.Models
{
    // Класс для хранения и вывода результатов анализа графа
    public class OutputData
    {
        // Идентификатор графа
        public int GraphId { get; }

        // Список, представляющий Эйлеров цикл, если он существует
        public List<int> EulerianCycle { get; }

        // Флаг, указывающий, существует ли Эйлеров цикл в графе
        public bool HasEulerianCycle { get; }

        // Время обработки данных (когда результаты были получены или обработаны)
        public DateTime ProcessingTime { get; }

        // Время выполнения алгоритма, вычисляющего Эйлеров цикл
        public TimeSpan ExecutionTime { get; }

        // Конструктор для инициализации объекта OutputData с заданными значениями
        public OutputData(int graphId, List<int> eulerianCycle, bool hasEulerianCycle,
            DateTime processingTime, TimeSpan executionTime)
        {
            // Устанавливаем идентификатор графа
            GraphId = graphId;

            // Устанавливаем список Эйлерова цикла (или пустой список, если его нет)
            EulerianCycle = eulerianCycle;

            // Устанавливаем флаг наличия Эйлерова цикла в графе
            HasEulerianCycle = hasEulerianCycle;

            // Устанавливаем время, когда была проведена обработка данных
            ProcessingTime = processingTime;

            // Устанавливаем время выполнения алгоритма
            ExecutionTime = executionTime;
        }

        // Метод для сохранения результатов анализа в файл
        public void SaveToFile(string outputPath)
        {
            var resultBuilder = new StringBuilder();
            
            // Добавляем заголовок с результатами анализа графа
            resultBuilder.AppendLine($"Graph Analysis Results - Graph {GraphId}");
            resultBuilder.AppendLine($"Processing Date: {ProcessingTime:yyyy-MM-dd HH:mm:ss}");
            resultBuilder.AppendLine($"Execution Time: {ExecutionTime.TotalMilliseconds:F2} ms");
            resultBuilder.AppendLine();

            // Если Эйлеров цикл найден, выводим его, иначе сообщаем, что он отсутствует
            if (HasEulerianCycle)
            {
                resultBuilder.AppendLine("Eulerian Cycle Found:");
                resultBuilder.AppendLine(string.Join(" -> ", EulerianCycle)); // Переводим список в строку
            }
            else
            {
                resultBuilder.AppendLine("No Eulerian Cycle exists in this graph.");
            }

            // Формируем имя файла для сохранения результатов с меткой времени
            var fileName = $"graph_{GraphId}_result_{ProcessingTime:yyyyMMdd_HHmmss}.txt";

            // Полный путь для сохранения файла с результатами
            var fullPath = Path.Combine(outputPath, fileName);

            // Создаем директорию для сохранения файла, если она не существует
            Directory.CreateDirectory(outputPath);

            // Сохраняем результаты в файл
            File.WriteAllText(fullPath, resultBuilder.ToString());
        }
    }
}
