using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System;
using Collector;
using Event;

namespace Sql {
    class Object {
        public static List<Clan> clans() {
            List<Clan> clans = new List<Clan>();
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();       
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT [Name]");
                    sb.Append("FROM [dbo].[Clan] ");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								clans.Add(new Clan(reader["Name"].ToString()));
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return clans;
        }
        public static List<Event.Event> events() {
            List<Event.Event> events = new List<Event.Event>();
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();       
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * ");
                    sb.Append("FROM [dbo].[Event] ");
                    string sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int eventID = Int32.Parse(reader["ID"].ToString());
                                if (reader["isTeamed"].ToString() == "false") {
                                    
								    events.Add(new Event.Event(reader["Name"].ToString(), eventID, usersFromEventUserTable(eventID).ToArray()));
                                } else {

                                    events.Add(new Event.Event(reader["Name"].ToString(), eventID, teamsFromEventUserTable(eventID).ToArray()));
                                }
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return events;
        }
        public static User userFromUserTable(string Username) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                int[] skills = new int[27];
                long overallXP = 0;
                int ClanID = 0;
                DateTime skillTime = new DateTime();
                using (SqlCommand commandGet = new SqlCommand(String.getUserSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@Name", Username);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            skills[0] = Int32.Parse(reader["Attack"].ToString());
                            skills[1] = Int32.Parse(reader["Strength"].ToString());
                            skills[2] = Int32.Parse(reader["Defence"].ToString());
                            skills[3] = Int32.Parse(reader["Ranged"].ToString());
                            skills[4] = Int32.Parse(reader["Prayer"].ToString());
                            skills[5] = Int32.Parse(reader["Magic"].ToString());
                            skills[6] = Int32.Parse(reader["Constitution"].ToString());
                            skills[7] = Int32.Parse(reader["Crafting"].ToString());
                            skills[8] = Int32.Parse(reader["Mining"].ToString());
                            skills[9] = Int32.Parse(reader["Smithing"].ToString());
                            skills[10] = Int32.Parse(reader["Fishing"].ToString());
                            skills[11] = Int32.Parse(reader["Cooking"].ToString());
                            skills[12] = Int32.Parse(reader["Firemaking"].ToString());
                            skills[13] = Int32.Parse(reader["Woodcutting"].ToString());
                            skills[14] = Int32.Parse(reader["Runecrafting"].ToString());
                            skills[15] = Int32.Parse(reader["Dungeoneering"].ToString());
                            skills[16] = Int32.Parse(reader["Agility"].ToString());
                            skills[17] = Int32.Parse(reader["Herblore"].ToString());
                            skills[18] = Int32.Parse(reader["Thieving"].ToString());
                            skills[19] = Int32.Parse(reader["Fletching"].ToString());
                            skills[20] = Int32.Parse(reader["Slayer"].ToString());
                            skills[21] = Int32.Parse(reader["Farming"].ToString());
                            skills[22] = Int32.Parse(reader["Construction"].ToString());
                            skills[23] = Int32.Parse(reader["Hunter"].ToString());
                            skills[24] = Int32.Parse(reader["Summoning"].ToString());
                            skills[25] = Int32.Parse(reader["Divination"].ToString());
                            skills[26] = Int32.Parse(reader["Invention"].ToString());
                            overallXP = Int64.Parse(reader["Overall"].ToString());
                            ClanID = Int32.Parse(reader["ClanID"].ToString());
                            skillTime = DateTime.Parse(reader["SkillTime"].ToString());
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand(String.getClanNameFromClanIDSQL(), connection))
                {
                    command.Parameters.AddWithValue("@ID", ClanID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User(Username, reader["Name"].ToString(), skills, overallXP, skillTime);
                        }
                    }
                } 
                return new User(Username, skills, overallXP, skillTime);
            }
        }
        public static List<User> usersFromUserTable(string clanName) {
            int clanID = 0;
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                using (SqlCommand command = new SqlCommand(String.getClanIDFromClanNameSQL(), connection))
                {
                    command.Parameters.AddWithValue("@Name", clanName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clanID = Int32.Parse(reader["ID"].ToString());
                        }
                    }
                } 
                if (clanID == 0) {
                    Console.WriteLine("Invalid Clan");
                    return null;
                }
                using (SqlCommand commandGet = new SqlCommand(String.getUsersInClanSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@clanID", clanID);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = "";
                            int[] skills = new int[27];
                            long overallXP = 0;
                            DateTime skillTime = new DateTime();
                            username = reader["Name"].ToString();
                            skills[0] = Int32.Parse(reader["Attack"].ToString());
                            skills[1] = Int32.Parse(reader["Strength"].ToString());
                            skills[2] = Int32.Parse(reader["Defence"].ToString());
                            skills[3] = Int32.Parse(reader["Ranged"].ToString());
                            skills[4] = Int32.Parse(reader["Prayer"].ToString());
                            skills[5] = Int32.Parse(reader["Magic"].ToString());
                            skills[6] = Int32.Parse(reader["Constitution"].ToString());
                            skills[7] = Int32.Parse(reader["Crafting"].ToString());
                            skills[8] = Int32.Parse(reader["Mining"].ToString());
                            skills[9] = Int32.Parse(reader["Smithing"].ToString());
                            skills[10] = Int32.Parse(reader["Fishing"].ToString());
                            skills[11] = Int32.Parse(reader["Cooking"].ToString());
                            skills[12] = Int32.Parse(reader["Firemaking"].ToString());
                            skills[13] = Int32.Parse(reader["Woodcutting"].ToString());
                            skills[14] = Int32.Parse(reader["Runecrafting"].ToString());
                            skills[15] = Int32.Parse(reader["Dungeoneering"].ToString());
                            skills[16] = Int32.Parse(reader["Agility"].ToString());
                            skills[17] = Int32.Parse(reader["Herblore"].ToString());
                            skills[18] = Int32.Parse(reader["Thieving"].ToString());
                            skills[19] = Int32.Parse(reader["Fletching"].ToString());
                            skills[20] = Int32.Parse(reader["Slayer"].ToString());
                            skills[21] = Int32.Parse(reader["Farming"].ToString());
                            skills[22] = Int32.Parse(reader["Construction"].ToString());
                            skills[23] = Int32.Parse(reader["Hunter"].ToString());
                            skills[24] = Int32.Parse(reader["Summoning"].ToString());
                            skills[25] = Int32.Parse(reader["Divination"].ToString());
                            skills[26] = Int32.Parse(reader["Invention"].ToString());
                            overallXP = Int64.Parse(reader["Overall"].ToString());
                            skillTime = DateTime.Parse(reader["SkillTime"].ToString());
                            users.Add(new User(username, clanName, skills, overallXP, skillTime));
                        }
                    }
                }
                return users;
            }
        }
        public static EventUser userFromEventUserTable(string Username, int eventID) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                int[] skills = new int[27];
                int[] points = new int[27];
                long overallXP = 0;
                long overallPoints = 0;
                int TeamID = 0;
                DateTime skillTime = new DateTime();
                using (SqlCommand commandGet = new SqlCommand(String.getEventUserSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@Name", Username);
                    commandGet.Parameters.AddWithValue("@eventID", eventID);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            skills[0] = Int32.Parse(reader["AttackXP"].ToString());
                            skills[1] = Int32.Parse(reader["StrengthXP"].ToString());
                            skills[2] = Int32.Parse(reader["DefenceXP"].ToString());
                            skills[3] = Int32.Parse(reader["RangedXP"].ToString());
                            skills[4] = Int32.Parse(reader["PrayerXP"].ToString());
                            skills[5] = Int32.Parse(reader["MagicXP"].ToString());
                            skills[6] = Int32.Parse(reader["ConstitutionXP"].ToString());
                            skills[7] = Int32.Parse(reader["CraftingXP"].ToString());
                            skills[8] = Int32.Parse(reader["MiningXP"].ToString());
                            skills[9] = Int32.Parse(reader["SmithingXP"].ToString());
                            skills[10] = Int32.Parse(reader["FishingXP"].ToString());
                            skills[11] = Int32.Parse(reader["CookingXP"].ToString());
                            skills[12] = Int32.Parse(reader["FiremakingXP"].ToString());
                            skills[13] = Int32.Parse(reader["WoodcuttingXP"].ToString());
                            skills[14] = Int32.Parse(reader["RunecraftingXP"].ToString());
                            skills[15] = Int32.Parse(reader["DungeoneeringXP"].ToString());
                            skills[16] = Int32.Parse(reader["AgilityXP"].ToString());
                            skills[17] = Int32.Parse(reader["HerbloreXP"].ToString());
                            skills[18] = Int32.Parse(reader["ThievingXP"].ToString());
                            skills[19] = Int32.Parse(reader["FletchingXP"].ToString());
                            skills[20] = Int32.Parse(reader["SlayerXP"].ToString());
                            skills[21] = Int32.Parse(reader["FarmingXP"].ToString());
                            skills[22] = Int32.Parse(reader["ConstructionXP"].ToString());
                            skills[23] = Int32.Parse(reader["HunterXP"].ToString());
                            skills[24] = Int32.Parse(reader["SummoningXP"].ToString());
                            skills[25] = Int32.Parse(reader["DivinationXP"].ToString());
                            skills[26] = Int32.Parse(reader["InventionXP"].ToString());
                            overallXP = Int64.Parse(reader["OverallXP"].ToString());
                            points[0] = Int32.Parse(reader["AttackPoints"].ToString());
                            points[1] = Int32.Parse(reader["StrengthPoints"].ToString());
                            points[2] = Int32.Parse(reader["DefencePoints"].ToString());
                            points[3] = Int32.Parse(reader["RangedPoints"].ToString());
                            points[4] = Int32.Parse(reader["PrayerPoints"].ToString());
                            points[5] = Int32.Parse(reader["MagicPoints"].ToString());
                            points[6] = Int32.Parse(reader["ConstitutionPoints"].ToString());
                            points[7] = Int32.Parse(reader["CraftingPoints"].ToString());
                            points[8] = Int32.Parse(reader["MiningPoints"].ToString());
                            points[9] = Int32.Parse(reader["SmithingPoints"].ToString());
                            points[10] = Int32.Parse(reader["FishingPoints"].ToString());
                            points[11] = Int32.Parse(reader["CookingPoints"].ToString());
                            points[12] = Int32.Parse(reader["FiremakingPoints"].ToString());
                            points[13] = Int32.Parse(reader["WoodcuttingPoints"].ToString());
                            points[14] = Int32.Parse(reader["RunecraftingPoints"].ToString());
                            points[15] = Int32.Parse(reader["DungeoneeringPoints"].ToString());
                            points[16] = Int32.Parse(reader["AgilityPoints"].ToString());
                            points[17] = Int32.Parse(reader["HerblorePoints"].ToString());
                            points[18] = Int32.Parse(reader["ThievingPoints"].ToString());
                            points[19] = Int32.Parse(reader["FletchingPoints"].ToString());
                            points[20] = Int32.Parse(reader["SlayerPoints"].ToString());
                            points[21] = Int32.Parse(reader["FarmingPoints"].ToString());
                            points[22] = Int32.Parse(reader["ConstructionPoints"].ToString());
                            points[23] = Int32.Parse(reader["HunterPoints"].ToString());
                            points[24] = Int32.Parse(reader["SummoningPoints"].ToString());
                            points[25] = Int32.Parse(reader["DivinationPoints"].ToString());
                            points[26] = Int32.Parse(reader["InventionPoints"].ToString());
                            overallPoints = Int64.Parse(reader["OverallPoints"].ToString());
                            TeamID = Int32.Parse(reader["TeamID"].ToString());
                            skillTime = DateTime.Parse(reader["SkillTime"].ToString());
                        }
                    }
                }
                return new EventUser(Username, skills, points, overallXP, overallPoints, skillTime);
            }
        }
        public static List<EventUser> usersFromEventUserTable(int eventID) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                List<EventUser> users = new List<EventUser>();
                using (SqlCommand commandGet = new SqlCommand(String.getEventUsersSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@eventID", eventID);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int[] skills = new int[27];
                            int[] points = new int[27];
                            long overallXP = 0;
                            long overallPoints = 0;
                            int teamID = 0;
                            string Username = "";
                            DateTime skillTime = new DateTime();
                            Username = reader["Username"].ToString();
                            skills[0] = Int32.Parse(reader["AttackXP"].ToString());
                            skills[1] = Int32.Parse(reader["StrengthXP"].ToString());
                            skills[2] = Int32.Parse(reader["DefenceXP"].ToString());
                            skills[3] = Int32.Parse(reader["RangedXP"].ToString());
                            skills[4] = Int32.Parse(reader["PrayerXP"].ToString());
                            skills[5] = Int32.Parse(reader["MagicXP"].ToString());
                            skills[6] = Int32.Parse(reader["ConstitutionXP"].ToString());
                            skills[7] = Int32.Parse(reader["CraftingXP"].ToString());
                            skills[8] = Int32.Parse(reader["MiningXP"].ToString());
                            skills[9] = Int32.Parse(reader["SmithingXP"].ToString());
                            skills[10] = Int32.Parse(reader["FishingXP"].ToString());
                            skills[11] = Int32.Parse(reader["CookingXP"].ToString());
                            skills[12] = Int32.Parse(reader["FiremakingXP"].ToString());
                            skills[13] = Int32.Parse(reader["WoodcuttingXP"].ToString());
                            skills[14] = Int32.Parse(reader["RunecraftingXP"].ToString());
                            skills[15] = Int32.Parse(reader["DungeoneeringXP"].ToString());
                            skills[16] = Int32.Parse(reader["AgilityXP"].ToString());
                            skills[17] = Int32.Parse(reader["HerbloreXP"].ToString());
                            skills[18] = Int32.Parse(reader["ThievingXP"].ToString());
                            skills[19] = Int32.Parse(reader["FletchingXP"].ToString());
                            skills[20] = Int32.Parse(reader["SlayerXP"].ToString());
                            skills[21] = Int32.Parse(reader["FarmingXP"].ToString());
                            skills[22] = Int32.Parse(reader["ConstructionXP"].ToString());
                            skills[23] = Int32.Parse(reader["HunterXP"].ToString());
                            skills[24] = Int32.Parse(reader["SummoningXP"].ToString());
                            skills[25] = Int32.Parse(reader["DivinationXP"].ToString());
                            skills[26] = Int32.Parse(reader["InventionXP"].ToString());
                            overallXP = Int64.Parse(reader["OverallXP"].ToString());
                            if (!reader["AttackPoints"].ToString().Equals(""))
                                points[0] = Int32.Parse(reader["AttackPoints"].ToString());
                            if (!reader["StrengthPoints"].ToString().Equals(""))
                                points[1] = Int32.Parse(reader["StrengthPoints"].ToString());
                            if (!reader["DefencePoints"].ToString().Equals(""))
                                points[2] = Int32.Parse(reader["DefencePoints"].ToString());
                            if (!reader["RangedPoints"].ToString().Equals(""))
                                points[3] = Int32.Parse(reader["RangedPoints"].ToString());
                            if (!reader["PrayerPoints"].ToString().Equals(""))
                                points[4] = Int32.Parse(reader["PrayerPoints"].ToString());
                            if (!reader["MagicPoints"].ToString().Equals(""))
                                points[5] = Int32.Parse(reader["MagicPoints"].ToString());
                            if (!reader["ConstitutionPoints"].ToString().Equals(""))
                                points[6] = Int32.Parse(reader["ConstitutionPoints"].ToString());
                            if (!reader["CraftingPoints"].ToString().Equals(""))
                                points[7] = Int32.Parse(reader["CraftingPoints"].ToString());
                            if (!reader["MiningPoints"].ToString().Equals(""))
                                points[8] = Int32.Parse(reader["MiningPoints"].ToString());
                            if (!reader["SmithingPoints"].ToString().Equals(""))
                                points[9] = Int32.Parse(reader["SmithingPoints"].ToString());
                            if (!reader["FishingPoints"].ToString().Equals(""))
                                points[10] = Int32.Parse(reader["FishingPoints"].ToString());
                            if (!reader["CookingPoints"].ToString().Equals(""))
                                points[11] = Int32.Parse(reader["CookingPoints"].ToString());
                            if (!reader["FiremakingPoints"].ToString().Equals(""))
                                points[12] = Int32.Parse(reader["FiremakingPoints"].ToString());
                            if (!reader["WoodcuttingPoints"].ToString().Equals(""))
                                points[13] = Int32.Parse(reader["WoodcuttingPoints"].ToString());
                            if (!reader["RunecraftingPoints"].ToString().Equals(""))
                                points[14] = Int32.Parse(reader["RunecraftingPoints"].ToString());
                            if (!reader["DungeoneeringPoints"].ToString().Equals(""))
                                points[15] = Int32.Parse(reader["DungeoneeringPoints"].ToString());
                            if (!reader["AgilityPoints"].ToString().Equals(""))
                                points[16] = Int32.Parse(reader["AgilityPoints"].ToString());
                            if (!reader["HerblorePoints"].ToString().Equals(""))
                                points[17] = Int32.Parse(reader["HerblorePoints"].ToString());
                            if (!reader["ThievingPoints"].ToString().Equals(""))
                                points[18] = Int32.Parse(reader["ThievingPoints"].ToString());
                            if (!reader["FletchingPoints"].ToString().Equals(""))
                                points[19] = Int32.Parse(reader["FletchingPoints"].ToString());
                            if (!reader["SlayerPoints"].ToString().Equals(""))
                                points[20] = Int32.Parse(reader["SlayerPoints"].ToString());
                            if (!reader["FarmingPoints"].ToString().Equals(""))
                                points[21] = Int32.Parse(reader["FarmingPoints"].ToString());
                            if (!reader["ConstructionPoints"].ToString().Equals(""))
                                points[22] = Int32.Parse(reader["ConstructionPoints"].ToString());
                            if (!reader["HunterPoints"].ToString().Equals(""))
                                points[23] = Int32.Parse(reader["HunterPoints"].ToString());
                            if (!reader["SummoningPoints"].ToString().Equals(""))
                                points[24] = Int32.Parse(reader["SummoningPoints"].ToString());
                            if (!reader["DivinationPoints"].ToString().Equals(""))
                                points[25] = Int32.Parse(reader["DivinationPoints"].ToString());
                            if (!reader["InventionPoints"].ToString().Equals(""))
                                points[26] = Int32.Parse(reader["InventionPoints"].ToString());
                            if (!reader["OverallPoints"].ToString().Equals(""))
                                overallPoints = Int64.Parse(reader["OverallPoints"].ToString());
                            teamID = Int32.Parse(reader["TeamID"].ToString());
                            skillTime = DateTime.Parse(reader["SkillTime"].ToString());
                            users.Add(new EventUser(Username, skills, points, overallXP, overallPoints, skillTime, teamID));
                        }
                    }
                }
                return users;
            }
        }
        public static string teamName(int teamID) {
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.getTeamNameFromIDSQL(), connection))
                    {
                        command.Parameters.AddWithValue("@ID", teamID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								return reader["Name"].ToString();
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }
        public static List<Team> teamsFromEventUserTable(int eventID) {
            List<Team> teams = new List<Team>();
            var users = usersFromEventUserTable(eventID);
            List<EventUser>[] usersTeamed = new List<EventUser>[users.Count];
            foreach (EventUser user in users) {
                usersTeamed[user.teamID].Add(user);
            }
            foreach (List<EventUser> team in usersTeamed) {
                var teamArray = team.ToArray();
                if (teamArray != null)
                    teams.Add(new Team(teamName(teamArray[0].teamID), teamArray));
            }
            return teams;
        }
        public static int eventIDFromName(string eventName) {
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.getEventFromNameSQL(), connection))
                    {
                        command.Parameters.AddWithValue("@name", eventName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								return Int32.Parse(reader["ID"].ToString());
                            }
                        }
                    }                    
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }
        public static List<Event.Event> eventsNotEnded() {
            List<Event.Event> events = new List<Event.Event>();
            try {
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.eventsNotEnded(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["isTeamed"].Equals(false)) {
								    events.Add(new Event.Event(reader["Name"].ToString(),
                                        usersFromEventUserTable(Int32.Parse(reader["ID"].ToString())).ToArray()));
                                } else {
                                    events.Add(new Event.Event(reader["Name"].ToString(),
                                        teamsFromEventUserTable(Int32.Parse(reader["ID"].ToString())).ToArray()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            } 
            return events;
        }
    }
}