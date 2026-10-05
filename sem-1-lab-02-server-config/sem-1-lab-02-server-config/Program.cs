namespace sem_1_lab_02_server_config
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите кол-во игроков на сервере: ");
            string playersRaw = Console.ReadLine();
            int players = Convert.ToInt32(playersRaw);

            Console.Write("Введите кол-во оперативной памяти (Гб): ");
            string ramRaw = Console.ReadLine();
            int ram = Convert.ToInt32(ramRaw);

            Console.Write("Является ли сервер публичным? (1/0): ");
            string isPublicRaw = Console.ReadLine();
            bool isPublic = isPublicRaw == "1";

            Console.Write("Есть ли на сервере пароль? (1/0): ");
            string hasPasswordRaw = Console.ReadLine();
            bool hasPassword = hasPasswordRaw == "1";

            string status = CheckConfiguration(players, ram, isPublic, hasPassword);
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine($" {status}");
            Console.WriteLine("|------------------------------------------------|");

        }

        public static string CheckConfiguration(int players, int ram, bool isPublic, bool hasPassword)
        {
            if (players <= 0)
            {
                return "Запуск невозможен: количество игроков должно быть больше нуля.";
            }

            if (ram <= 1)
            {
                return "Запуск невозможен: серверу недостаточно оперативной памяти.";
            }

            if (isPublic && hasPassword)
            {
                return "Запуск возможен с предупреждением: публичный сервер защищён паролем.";
            }

            if (players > ram * 10) 
            {
                return "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
            }

            return "Сервер готов к запуску.";
        }
    }

}
