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
            Clan consentus = new Clan("consentus");
            foreach (User user in consentus.users) {
                Console.WriteLine(user.name);
            }
            consentus.users[3].update();
            consentus.users[2].update();
        }
    }
}
