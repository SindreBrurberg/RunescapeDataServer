using System;
using Handler;

namespace Collector {
    class User {
        public string name {get;}
        public int[] skills {get;}
        public long overall {get; private set;}
        public bool UserInfoFound {get; private set;}
        private int trie = 0;
        public User(string name) {
            this.name = name;
            skills = new int[27];
            update();
        }

        public void update() {
            while (trie <= 3 && !UserInfoFound) {
                trie++;
                Console.WriteLine(name);
                string UserInfo = Web.MakeAsyncRequest("https://apps.runescape.com/runemetrics/profile/profile?user=" + name + "&activities=0", "text/csv");
                if (UserInfo.Contains("error") || name == "Charms") { //Remember to remove the or case for charms
                    Console.WriteLine("User info errored out");
                    UserInfo = Web.MakeAsyncRequest("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + name, "text/csv");
                    if (UserInfo.Contains("error")) {
                        Console.WriteLine("User info errored out");
                        if (trie == 3) {
                            Console.WriteLine("User info errored out, moving to next user");
                            UserInfoFound = false;
                            break;
                        }
                    } else {
                        int i = -1;
                        foreach (string info in UserInfo.Split(new string[]{" ", "\r", "\n", "\r\n", Environment.NewLine}
                                                        , System.StringSplitOptions.RemoveEmptyEntries)) {
                            if (i <= 27) {           
                                var skill = info.Split(',');
                                if (Int32.Parse(skill[0]) != -1) {    
                                    if (i > -1) {
                                        skills[i] = Int32.Parse(skill[2]);
                                    } else {
                                        overall = Int64.Parse(skill[2]);
                                    }
                                }
                            }
                            i++;
                        }
                        UserInfoFound = true;
                    }
                } else {
                    foreach (string info in UserSkillsInfo(UserInfo, "skillvalues\":[{", "}]",new string[]{"},{"})) {
                        short id = Int16.Parse(info.Substring(info.IndexOf("id") + 4));
                        int xp = Int32.Parse(info.Substring(info.IndexOf("xp") + 4).Split(',')[0]);
                        skills[id] = xp;
                        overall += xp;
                    }
                    UserInfoFound = true;
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