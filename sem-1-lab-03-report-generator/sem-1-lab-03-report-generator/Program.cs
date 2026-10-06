namespace sem_1_lab_03_report_generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("event_server.log");
            Console.WriteLine(BuildReport(lines));
        }

        public static int WarnErrCounter(string[] lines, string marker)
        {
            int markerCount = 0;
            bool eventStarted = false;

            foreach (string line in lines)
            {
                if (line.Contains(marker) && eventStarted)
                {
                    markerCount++;
                }
                else if (line.Contains("Событие началось:"))
                {
                    eventStarted = true;
                }
                else if (line.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
                {
                    eventStarted = false;
                }
            }
            return markerCount;
        }

        public static string MvpOfTheMatch(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line.Contains("объявлены победителями"))
                {
                    int winnerStart = line.IndexOf("[Reward]") + "[Reward] ".Length;
                    int winnerEnd = line.IndexOf(" объявлены");
                    return line.Substring(winnerStart, winnerEnd - winnerStart);
                }
            }
            return "";
        }

        public static int WinnerExp(string[] lines)
        {
            string mvpIs = MvpOfTheMatch(lines);
            foreach (string line in lines)
            {
                if (line.Contains($"[Reward] {mvpIs} получили"))
                {
                    int expStart = line.IndexOf("получили ") + "получили ".Length;
                    int expEnd = line.IndexOf(" очков");
                    return int.Parse(line.Substring(expStart, expEnd - expStart));
                }
            }
            return 0;
        }

        public static string WinnerItem(string[] lines)
        {
            string mvpIs = MvpOfTheMatch(lines);
            foreach (string line in lines)
            {
                if (line.Contains($"[Loot] {mvpIs} получили ивентовый"))
                {
                    int itemStart = line.IndexOf("предмет: ") + "предмет: ".Length;
                    return line.Substring(itemStart);
                }
            }
            return "";
        }

        public static int LoserExp(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line.Contains("получили утешительную награду: "))
                {
                    int loserExpStart = line.IndexOf("награду: ") + "награду: ".Length;
                    int loserExpEnd = line.IndexOf(" очков");
                    return int.Parse(line.Substring(loserExpStart, loserExpEnd - loserExpStart));
                }
            }
            return 0;
        }

        public static DateTime StartDate(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line.Contains("Событие началось:"))
                {
                    return DateTime.Parse(line.Substring(0, 10));
                }
            }
            return DateTime.MinValue;
        }
        
        public static string BuildReport(string[] lines)
        {
            return ("# Итоги события: Восстание Ледяного Пламени\n\n") +
                    ($"Дата: {StartDate(lines).ToString("dd.MM.yyyy")}\n") +
                    ($"Победитель: {MvpOfTheMatch(lines)}\n") +
                    ($"Очки победителя: {WinnerExp(lines)}\n") +
                    ($"Ивентовый предмет: {WinnerItem(lines)}\n") +
                    ($"Утешительная награда Железных волков: {LoserExp(lines)} очков\n") +
                    ($"Предупреждений во время события: {WarnErrCounter(lines, "[Warning]")}\n") +
                    ($"Ошибок во время события: {WarnErrCounter(lines, "[Error]")}\n");
        }
    }
}

