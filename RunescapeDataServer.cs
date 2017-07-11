using System;
using System.IO;
using System.Text;
using System.Threading;
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
            var testTimer = new Timer(uppdateLoop, null, MillisecondsToNextHalfHouer(), 30*60*1000);
            Console.ReadLine();
        }
        private static int MillisecondsToNextHalfHouer() {
            DateTime now = DateTime.Now;
            return ((60 - now.Minute) % 30 * 60 - now.Second) * 1000 - now.Millisecond;
        }
        private static void config() {
            ConfigFile.init();
            Console.WriteLine("Config File done");
            clans = Sql.clans();
        }
        private static void uppdateLoop(Object stateInfo) {
            foreach (string clan in clans) {
                Clan consentus = new Clan(clan);
            }
        }
    }
    class StatusChecker
    {
        private int invokeCount;
        private int  maxCount;

        public StatusChecker(int count)
        {
            invokeCount  = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Checking status {1,2}.", 
                DateTime.Now.ToString("h:mm:ss.fffffff"), 
                (++invokeCount).ToString());

            if(invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }

}

