using System;
using Handler;
using Sql;

namespace Collector {
    class User {
        public string name {get;}
        public string clan {get;}
        public DateTime skillTime {get; private set;}
        public int[] skills {get;}
        public long overallXP {get; private set;}
        public bool UserInfoFound {get; private set;}
        private int trie = 0;
        public User(string name, string clan) {
            this.name = name;
            this.clan = clan;
            this.skills = new int[27];
            this.skillTime = DateTime.Now;
        }
        public User(string name, string clan, int[] skills, long overallXP, DateTime skillTime) {
            this.name = name;
            this.clan = clan;
            this.skills = skills;
            this.overallXP = overallXP;
            this.skillTime = skillTime;
        }
        public User(string name, int[] skills, long overallXP, DateTime skillTime) {
            this.name = name;
            this.skills = skills;
            this.overallXP = overallXP;
            this.skillTime = skillTime;
        }

        public void update() {
            Console.WriteLine("Uppdating user: {0}", name);
            while (trie <= 3 && !UserInfoFound) {
                trie++;
                string UserInfo = Web.MakeAsyncRequest("https://apps.runescape.com/runemetrics/profile/profile?user=" + name + "&activities=0", "text/csv");
                if (UserInfo.Contains("error")) {
                    Console.WriteLine("User info method one errored out, using method two");
                    try {
                        UserInfo = Web.MakeAsyncRequest("http://services.runescape.com/m=hiscore/index_lite.ws?player=" + name, "text/csv");
                    } catch (Exception e) {
                        UserInfo = "error";
                        Console.WriteLine(e);
                    }
                    if (UserInfo.Contains("error")) {
                        Console.WriteLine("User info method two errored out");
                        if (trie == 3) {
                            Console.WriteLine("User info errored out, moving to next user");
                            UserInfoFound = false;
                            break;
                        }
                    } else {
                        int i = -1;
                        foreach (string info in UserInfo.Split(new string[]{" ", "\r", "\n", "\r\n", Environment.NewLine}
                                                        , System.StringSplitOptions.RemoveEmptyEntries)) {
                            if (i < 27) {           
                                var skill = info.Split(',');
                                if (Int32.Parse(skill[0]) != -1) {    
                                    if (i > -1) {
                                        skills[i] = Int32.Parse(skill[2]);
                                    } else {
                                        overallXP = Int64.Parse(skill[2]);
                                    }
                                }
                            }
                            i++;
                        }
                        UserInfoFound = true;
                        updateSql();
                    }
                } else {
                    foreach (string info in UserSkillsInfo(UserInfo, "skillvalues\":[{", "}]", new string[]{"},{"})) {
                        short id = Int16.Parse(info.Substring(info.IndexOf("id") + 4));
                        string xps = info.Substring(info.IndexOf("xp") + 4).Split(',')[0];
                        int xp = Int32.Parse(xps.Length > 1 ? xps.Remove(xps.Length -1) : xps);
                        skills[id] = xp;
                        overallXP += xp; 
                    }
                    UserInfoFound = true;
                    updateSql();
                }
            }
        }
        private void updateSql() {
            skillTime = DateTime.Now;
            Command.updateUser(this);
        }
        private string[] UserSkillsInfo(string UserInfo, string start, string end, string[] sepatator) {
            return UserInfo.Substring(UserInfo.IndexOf(start) + start.Length)
                .Remove(UserInfo.IndexOf(end) - UserInfo.IndexOf(start) - start.Length)
                .Split(sepatator, System.StringSplitOptions.RemoveEmptyEntries);
        }
        public int xpForLevel(int level) {
			double total = 0;
			for (int i = 1; i < level; i++) {
				total += Math.Floor(i + 300 * Math.Pow(2, i /7.0));
			}
			return (int)Math.Floor(total / 4);
		}	
		public int level (int xp) {
			if (xpForLevel(99) < xp) {return 99;}
			for (int i = 1; i < 100; i++) {
				if (xpForLevel(i) > xp) { //kan bome med 1 xp
					return i-1;
				}
			}
			return 0;
		}
    }
}