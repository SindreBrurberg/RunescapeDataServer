using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Handler;
using Collector;

namespace RunescapeDataServer
{
    class Program
    {
        private static List<string> clans;
        static void Main(string[] args)
        {
            config();
            uppdateLoop();
            Console.ReadLine();
        }
        private static void config() {
            Config database = new Config();
            Sql.DataSource = "RunescapeMinigames.database.windows.net";
            Sql.Username = "Dethsanius";
            Sql.Password = "Pass!000";
            Sql.Catalog = "RunescapeMinigames";
            clans = Sql.clans();
        }
        private static void uppdateLoop() {
            foreach (string clan in clans) {
                Clan consentus = new Clan(clan);
            }
        }
    }
}
