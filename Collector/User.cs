using System;
using Handler;

namespace Collector {
    class User {
        public string name {get;}
        public int[] skills {get;}
        public User(string name) {
            this.name = name;
            skills = new int[27];
        }

        public void update() {
            Console.WriteLine(name);
            string UserInfo = Web.MakeAsyncRequest("https://apps.runescape.com/runemetrics/profile/profile?user=" + name + "&activities=0", "text/csv");
            if (UserInfo.Contains("error") || name == "Charms") { //Remember to remove the or case for charms
                Console.WriteLine("User info errored out");
                UserInfo = Web.MakeAsyncRequest("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + name, "text/csv");
                foreach (string info in UserInfo.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries)) {
                    Console.WriteLine(info);
                }
            } else {
                foreach (string info in UserSkillsInfo(UserInfo, "skillvalues\":[{", "}]",new string[]{"},{"})) {
                    Console.WriteLine(info);
                }
            }
        }
        private string[] UserSkillsInfo(string UserInfo, string start, string end, string[] sepatator) {
            return UserInfo.Substring(UserInfo.IndexOf(start) + start.Length)
                .Remove(UserInfo.IndexOf(end) - UserInfo.IndexOf(start) - start.Length)
                .Split(sepatator, System.StringSplitOptions.RemoveEmptyEntries);
        }
    }
}