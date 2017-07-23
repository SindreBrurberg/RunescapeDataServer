using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using Handler;
using Collector;
using Sql;
using ServerData;

namespace Server
{
    class Program
    {
        //public static List<string> clans;
        public static List<Clan> clans;
        public static List<string> clanNames;
        static void Main(string[] args)
        {
            config();
            uppdateLoop(null);
            Console.ReadLine();
            RequestServer.run();
            var testTimer = new Timer(uppdateLoop, null, MillisecondsToNextHalfHouer(), 30*60*1000);
            Console.ReadLine();
        }

        private static int MillisecondsToNextHalfHouer() {
            DateTime now = DateTime.Now;
            return ((60 - now.Minute) % 30 * 60 - now.Second) * 1000 - now.Millisecond;
        }
        private static void config() {
            ConfigFile.init();
            Console.WriteLine("Config File Loaded");
            clans = Sql.Object.clans();
            foreach (Clan clan in clans) {
                clanNames.Add(clan.name);
            }
        }
        private static void uppdateLoop(object stateInfo) {
            foreach (Clan clan in clans) {
                clan.update();
            }
        }
    }
}

