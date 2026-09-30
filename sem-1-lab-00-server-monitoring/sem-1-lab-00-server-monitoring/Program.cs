using System.Net.NetworkInformation;

namespace sem_1_lab_00_server_monitoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Нужно передать серваку данные, взять кол-во игроков, 
            //пинг, мб загруженность в %, кол-во затраченных денег за сутки (от кол-ва игроков)

            Console.WriteLine("|--------------------------------|");
            Console.WriteLine("| Get ready to start the server! |");
            Console.WriteLine("|--------------------------------|");

            Console.WriteLine("Start the server? (1/0): ");
            var status = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("How many players is there?: ");
            var players = Console.ReadLine();
            var Pl = Convert.ToDouble(players);
            Console.WriteLine("What is ping status?: ");
            var ping = Console.ReadLine();
            var BusyRam = (Pl * 100) / 1000; //При условии, что сервак держит 1к чел.
            var Money = BusyRam * 1.5; //пусть будет 1.5 бакса за 10 человек в час 
 
            
            Console.WriteLine("|--------------------------------------|");
            Console.WriteLine("    Server status:                ");
            Console.WriteLine($"Server has connection: {status}");
            Console.WriteLine($"Ping: {ping}");
            Console.WriteLine($"Players: {players}");
            Console.WriteLine($"Memory Utilization: {BusyRam}%");
            Console.WriteLine($"Money consuming: {Money}$");
            Console.WriteLine("|--------------------------------------|");
        }
    }
}
