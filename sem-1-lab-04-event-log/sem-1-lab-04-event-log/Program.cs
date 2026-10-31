using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sem_1_lab_04_event_log
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("event_server.log");
            LogEntry[] entries = ParseLog(lines);
            Console.WriteLine("-------------------------------------------"); //просто тестил работу методов
            Console.WriteLine(entries[116].Timestamp);
            Console.WriteLine(entries[116].Level);
            Console.WriteLine(entries[116].Category);
            Console.WriteLine(entries[116].Message);
            Console.WriteLine("-------------------------------------------");

            LogEntry[] errors = FilterByLevel(entries, "Error");
            Console.WriteLine($"кол-во записей ошибок: {errors.Length}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Сообщение об ошибке от: {errors[0].Timestamp}\n" +
                $"Категория информации из строки с ошибки: {errors[0].Category}\n" +
                $"Сообщение из строки с ошибкой: {errors[0].Message}");
            Console.WriteLine("-------------------------------------------");
        }

        public static LogEntry[] ParseLog(string[] lines) //метод для принятия строк файла и записи в массиве
        { 
            LogEntry[] entries = new LogEntry[lines.Length]; //делаем чтоб массив был на 1000 строк 
            for (int i = 0; i < lines.Length; i++) //для каждой строки повторяем код
            {
                LogEntry entry = new LogEntry(); //пустая запись для новой строки
                entry.Timestamp = DateTime.Parse(lines[i].Substring(0, 23)); //запись в категорию даты

                int firstSquareOpeningIndex = lines[i].IndexOf('['); //ищем индексы скобок предупреждений
                int firstSquareClosingIndex = lines[i].IndexOf(']');
                int levelLength = (firstSquareClosingIndex - firstSquareOpeningIndex - 1); //прост длина внутри скобок
                entry.Level = lines[i].Substring(firstSquareOpeningIndex + 1, levelLength); //какое у нас предупреждение

                int secondSquareOpeningIndex = lines[i].IndexOf('[', firstSquareOpeningIndex + 1); //после запятой число с которого начинаем поиск
                int secondSquareClosingIndex = lines[i].IndexOf(']', firstSquareClosingIndex + 1);
                int categoryLength = (secondSquareClosingIndex - secondSquareOpeningIndex - 1);
                entry.Category = lines[i].Substring(secondSquareOpeningIndex + 1, categoryLength); //откуда данные типо? сервер, база данных

                entry.Message = lines[i].Substring(secondSquareClosingIndex + 2); //сохраняем сообщение

                entries[i] = entry; //кладем заполненную запись в массив на место номер i
            }
            return entries; //возвращаем массив в вызов метода
        }

        public static LogEntry[] FilterByLevel(LogEntry[] entries, string level) //делаем метод который принимает наш массив записей и нужный уровень
        {
            int resultCount = 0; //счетчик сколько нужных записей в файле
            LogEntry[] temporary = new LogEntry[entries.Length]; //делаем временный массив размером с весь файл, чтобы хватило места
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level) //если у нас совпадает инфа о том что это та запись:
                {
                    temporary[resultCount] = entries[i]; //добавляем эту запись в временный массив
                    resultCount++;
                }
            }
            LogEntry[] filtered = new LogEntry[resultCount]; //тут уже делаем итоговый массив с кол-вом записей = кол-ву найденных нужных записей
            Array.Copy(temporary, filtered, resultCount); //используем чтобы скопировать записи из другого массива
            return filtered;
        }

        public static LogEntry[] FilterByCategory(LogEntry[] entries, string category) //тоже самое для остальных полей
        {
            int resultCount = 0; 
            LogEntry[] temporary = new LogEntry[entries.Length]; 
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Category == category) 
                {
                    temporary[resultCount] = entries[i]; 
                    resultCount++;
                }
            }
            LogEntry[] filtered = new LogEntry[resultCount]; 
            Array.Copy(temporary, filtered, resultCount); 
            return filtered;
        }

        public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date) 
        {
            int resultCount = 0;
            LogEntry[] temporary = new LogEntry[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Timestamp.Date == date.Date)
                {
                    temporary[resultCount] = entries[i];
                    resultCount++;
                }
            }
            LogEntry[] filtered = new LogEntry[resultCount];
            Array.Copy(temporary, filtered, resultCount);
            return filtered;
        }

        public static LogEntry[] Search(LogEntry[] entries, string text)
        {
            int resultCount = 0;
            LogEntry[] temporary = new LogEntry[entries.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Message.ToLower().Contains(text.ToLower())) //сравниваем что сообщение содержит нужное нам слово, делаем и сообщение и слово с маленькой буквы
                {
                    temporary[resultCount] = entries[i];
                    resultCount++;
                }
            }
            LogEntry[] filtered = new LogEntry[resultCount];
            Array.Copy(temporary, filtered, resultCount);
            return filtered;
        }

        public static int CountByLevel(LogEntry[] entries, string level) //считаем сколько записей искомого уровня
        {
            LogEntry[] levels = FilterByLevel(entries, level);
            return levels.Length;
        }

        public static string GetServerStatus(LogEntry[] entries) //выясняем статус по уровням в массиве записей
        {
            if (CountByLevel(entries, "Fatal") != 0)
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }

            else if (CountByLevel(entries, "Error") != 0)
            {
                return "Есть ошибки: требуется проверка";
            }

            else
            {
                return "Сервер работает штатно";
            }
        }
    }
}
