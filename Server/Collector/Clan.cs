using System;
using Handler;
using System.Collections.Generic;

namespace Collector {
    class Clan {
        public List<User> users {get;}
        public string name {get;}
        public long xp {get; private set;}
        private List<string> usernames = new List<string>();
        public Clan(string name) {
            this.name = name;
            this.users = new List<User>();
        }
        public void update() {
            xp = 0;
            string ClanUsers = Web.MakeAsyncRequest("http://services.runescape.com/m=clan-hiscores/members_lite.ws?clanName=" + name, "text/csv");
            string[] items = ClanUsers.Split(new string[]{",", "\r", "\n", "\r\n", Environment.NewLine}, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 4; i < items.Length; i++) {
                if (i % 4 == 0) {
                    string username = items[i].Replace("?", " ");
                    if (!usernames.Contains(username)) {
                        this.users.Add(new User(username, name));
                        usernames.Add(username);
                    }
                }
            }
            foreach (User user in users) {
                user.update();
                xp += user.overallXP;
            }
        }
    }  
}