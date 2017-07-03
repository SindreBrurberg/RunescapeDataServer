using System;
using System.Text;
using System.Threading.Tasks;
using Handler;
using Collector;

namespace RunescapeDataServer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string clan in Sql.clans()) {
                Clan consentus = new Clan(clan);
            }
        }
    }
}
