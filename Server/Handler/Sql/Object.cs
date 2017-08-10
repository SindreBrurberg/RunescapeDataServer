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
        public static string clanNameFromID(int clanID) {
            string clanName = "";
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();       
                    using (SqlCommand command = new SqlCommand(String.getClanNameFromClanIDSQL(), connection))
                    {
                        command.Parameters.AddWithValue("@ID", clanID);
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
            return clanName;
        }
        public static int clanIDFromName(string name) {
            try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();       
                    using (SqlCommand command = new SqlCommand(String.getClanIDFromClanNameSQL(), connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
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
        private static User userFromReader(SqlDataReader reader) {
            int[] skills = new int[27];
            long overall = 0;
            int clanID = 0;
            string Username = "";
            DateTime skillTime = new DateTime();
            Username = reader["Name"].ToString();
            skills = exstractInt("Attack", skills, 0, reader);
            skills = exstractInt("Strength", skills, 1, reader);
            skills = exstractInt("Defence", skills, 2, reader);
            skills = exstractInt("Ranged", skills, 3, reader);
            skills = exstractInt("Prayer", skills, 4, reader);
            skills = exstractInt("Magic", skills, 5, reader);
            skills = exstractInt("Constitution", skills, 6, reader);
            skills = exstractInt("Crafting", skills, 7, reader);
            skills = exstractInt("Mining", skills, 8, reader);
            skills = exstractInt("Smithing", skills, 9, reader);
            skills = exstractInt("Fishing", skills, 10, reader);
            skills = exstractInt("Cooking", skills, 11, reader);
            skills = exstractInt("Firemaking", skills, 12, reader);
            skills = exstractInt("Woodcutting", skills, 13, reader);
            skills = exstractInt("Runecrafting", skills, 14, reader);
            skills = exstractInt("Dungeoneering", skills, 15, reader);
            skills = exstractInt("Agility", skills, 16, reader);
            skills = exstractInt("Herblore", skills, 17, reader);
            skills = exstractInt("Thieving", skills, 18, reader);
            skills = exstractInt("Fletching", skills, 19, reader);
            skills = exstractInt("Slayer", skills, 20, reader);
            skills = exstractInt("Farming", skills, 21, reader);
            skills = exstractInt("Construction", skills, 22, reader);
            skills = exstractInt("Hunter", skills, 23, reader);
            skills = exstractInt("Summoning", skills, 24, reader);
            skills = exstractInt("Divination", skills, 25, reader);
            skills = exstractInt("Invention", skills, 26, reader);
            overall = Int64.Parse(exstractElement("Overall", reader));
            clanID = Int32.Parse(exstractElement("ClanID", reader) == null ? "0" : exstractElement("ClanID", reader));
            skillTime = DateTime.Parse(reader["SkillTime"].ToString());
            return new User(Username, clanID, skills, overall, skillTime);
        }
        public static User userFromUserTable(string Username) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();
                using (SqlCommand commandGet = new SqlCommand(String.getUserSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@Name", Username);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return userFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }
        public static List<User> usersFromUserTable(string clanName) {
            int clanID = clanIDFromName(clanName);
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                using (SqlCommand commandGet = new SqlCommand(String.getUsersInClanSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@clanID", clanID);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(userFromReader(reader));
                        }
                    }
                }
                return users;
            }
        }
        private static EventUser eventUserFromReader(SqlDataReader reader) {
            int[] skills = new int[27];
            int[] points = new int[27];
            long overallXP = 0;
            long overallPoints = 0;
            int teamID = 0;
            int clanID = 0;
            string Username = "";
            DateTime skillTime = new DateTime();
            var user = userFromReader(reader);
            Username = user.name;
            clanID = user.clan;
            skills = user.skills;
            overallXP = user.overallXP;
            skillTime = user.skillTime;
            points = exstractInt("AttackPoints", points, 0, reader);
            points = exstractInt("StrengthPoints", points, 1, reader);
            points = exstractInt("DefencePoints", points, 2, reader);
            points = exstractInt("RangedPoints", points, 3, reader);
            points = exstractInt("PrayerPoints", points, 4, reader);
            points = exstractInt("MagicPoints", points, 5, reader);
            points = exstractInt("ConstitutionPoints", points, 6, reader);
            points = exstractInt("CraftingPoints", points, 7, reader);
            points = exstractInt("MiningPoints", points, 8, reader);
            points = exstractInt("SmithingPoints", points, 9, reader);
            points = exstractInt("FishingPoints", points, 10, reader);
            points = exstractInt("CookingPoints", points, 11, reader);
            points = exstractInt("FiremakingPoints", points, 12, reader);
            points = exstractInt("WoodcuttingPoints", points, 13, reader);
            points = exstractInt("RunecraftingPoints", points, 14, reader);
            points = exstractInt("DungeoneeringPoints", points, 15, reader);
            points = exstractInt("AgilityPoints", points, 16, reader);
            points = exstractInt("HerblorePoints", points, 17, reader);
            points = exstractInt("ThievingPoints", points, 18, reader);
            points = exstractInt("FletchingPoints", points, 19, reader);
            points = exstractInt("SlayerPoints", points, 20, reader);
            points = exstractInt("FarmingPoints", points, 21, reader);
            points = exstractInt("ConstructionPoints", points, 22, reader);
            points = exstractInt("HunterPoints", points, 23, reader);
            points = exstractInt("SummoningPoints", points, 24, reader);
            points = exstractInt("DivinationPoints", points, 25, reader);
            points = exstractInt("InventionPoints", points, 26, reader);
            overallPoints = Int64.Parse(exstractElement("OverallPoints", reader) 
                == null ? "0" : exstractElement("OverallPoints", reader));
            teamID = Int32.Parse(exstractElement("TeamID", reader));
            return new EventUser(Username, clanID, skills, points, overallXP, overallPoints, skillTime);
        }
        public static EventUser userFromEventUserTable(string Username, int eventID) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                using (SqlCommand commandGet = new SqlCommand(String.getEventUserSQL(), connection))
                {
                    commandGet.Parameters.AddWithValue("@Name", Username);
                    commandGet.Parameters.AddWithValue("@eventID", eventID);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return eventUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
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
                            users.Add(eventUserFromReader(reader));
                        }
                    }
                }
                return users;
            }
        }
        private static int[] exstractInt(string name, int[] value, int id, SqlDataReader reader) {
            try {
                value[id] = Int32.Parse(exstractElement(name, reader));
            } catch (Exception e) {
                if (!e.ToString().Contains("Value cannot be null"))
                    Console.WriteLine(e);
            }
            return value;
        }
        private static string exstractElement(string name, SqlDataReader reader) {
            try {
                if (!reader[name].ToString().Equals(""))
                    return reader[name].ToString();
            } catch (Exception e) {
                if (!e.ToString().Contains("System.IndexOutOfRangeException: ClanID"))
                    Console.WriteLine(e);
            }
            return null;
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