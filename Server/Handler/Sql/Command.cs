using System;
using System.Data.SqlClient;
using Server;
using Event;
using Collector;

namespace Sql {
    class Command {
        private static void runSQLQuerry(string sql, User user, int ClanID) {
            using (SqlConnection connection = new SqlConnection(Connection.CS())) 
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // The structure of this is because of the ID per skill!!!
                    command.Parameters.AddWithValue(ClanID == 0 ? "@Username" : "@Name", user.name);
                    command.Parameters.AddWithValue("@Attack", user.skills[0]);
                    command.Parameters.AddWithValue("@Defence", user.skills[1]);
                    command.Parameters.AddWithValue("@Strength", user.skills[2]);
                    command.Parameters.AddWithValue("@Constitution", user.skills[3]);
                    command.Parameters.AddWithValue("@Ranged", user.skills[4]);
                    command.Parameters.AddWithValue("@Prayer", user.skills[5]);
                    command.Parameters.AddWithValue("@Magic", user.skills[6]);
                    command.Parameters.AddWithValue("@Cooking", user.skills[7]);
                    command.Parameters.AddWithValue("@Woodcutting", user.skills[8]);
                    command.Parameters.AddWithValue("@Fletching", user.skills[9]);
                    command.Parameters.AddWithValue("@Fishing", user.skills[10]);
                    command.Parameters.AddWithValue("@Firemaking", user.skills[11]);
                    command.Parameters.AddWithValue("@Crafting", user.skills[12]);
                    command.Parameters.AddWithValue("@Smithing", user.skills[13]);
                    command.Parameters.AddWithValue("@Mining", user.skills[14]);
                    command.Parameters.AddWithValue("@Herblore", user.skills[15]);
                    command.Parameters.AddWithValue("@Agility", user.skills[16]);
                    command.Parameters.AddWithValue("@Thieving", user.skills[17]);
                    command.Parameters.AddWithValue("@Slayer", user.skills[18]);
                    command.Parameters.AddWithValue("@Farming", user.skills[19]);
                    command.Parameters.AddWithValue("@Runecrafting", user.skills[20]);
                    command.Parameters.AddWithValue("@Hunter", user.skills[21]);
                    command.Parameters.AddWithValue("@Construction", user.skills[22]);
                    command.Parameters.AddWithValue("@Summoning", user.skills[23]);
                    command.Parameters.AddWithValue("@Dungeoneering", user.skills[24]);
                    command.Parameters.AddWithValue("@Divination", user.skills[25]);
                    command.Parameters.AddWithValue("@Invention ", user.skills[26]);
                    command.Parameters.AddWithValue("@Overall", user.overallXP);
                    if (ClanID != 0)
                        command.Parameters.AddWithValue("@ClanId", ClanID);
                    command.Parameters.AddWithValue("@SkillTime", user.skillTime);
                    command.ExecuteNonQuery();
                }
            }
        }
		public static void updateUser(User user) {
			try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open(); 
                    SqlCommand cmdCount = new SqlCommand("SELECT count(*) from [dbo].[User] WHERE name = @Name", connection);
                    cmdCount.Parameters.AddWithValue("@Name", user.name);
                    int count = (int)cmdCount.ExecuteScalar();
                    if (count > 0) {
                        insertNewUserTimeFromUser(user.name);
                        using (SqlCommand commandGet = new SqlCommand(String.getUserSQL(), connection)){
                            commandGet.Parameters.AddWithValue("@Name", user.name);
                            using (SqlDataReader reader = commandGet.ExecuteReader())
                            {
                                while (reader.Read()){
                                    if (Int64.Parse(reader["Overall"].ToString()) < Int64.Parse(user.overallXP.ToString())) 
                                    {
                                        runSQLQuerry(String.updateUserSQL(), user, user.clan);
                                    }     
                                }
                            }
                        }
                    } else {
                        runSQLQuerry(String.insertUserSQL(), user, user.clan);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
		}
		public static void insertNewUserTimeFromUser(string name) {
			try 
            { 
                User user = Sql.Object.userFromUserTable(name);
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    Boolean isNew = false;
                    using (SqlCommand commandGetTime = new SqlCommand(String.getUserTimeNewestSQL(), connection))
                    {
                        commandGetTime.Parameters.AddWithValue("@Username", name);
                        using (SqlDataReader readerTime = commandGetTime.ExecuteReader())
                        {
                            Console.WriteLine("Reading information about {0} from user table", name);
                            Boolean reading = false;
                            while (readerTime.Read())
                            {
                                reading = true;
                                if (!user.skillTime.Equals(readerTime["SkillTime"]))
                                    isNew = true;
                            }
                            if (!reading)
                                isNew = true;
                        }
                    }
                    if (isNew) {
                        runSQLQuerry(String.insertUserTimeSQL(), user, 0);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
		}
        public static void insertNewEventUserFromUser(string name, int eventID, int? teamID) {
			try 
            { 
                User user = Sql.Object.userFromUserTable(name);
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.insertNewEventUserSQL(), connection))
                    {
                        Console.WriteLine("Inserting eventuser {0}", user.name);
                        // The structure of this is because of the ID per skill!!!
                        command.Parameters.AddWithValue("@Name", user.name);
                        command.Parameters.AddWithValue("@Attack", user.skills[0]);
                        command.Parameters.AddWithValue("@Defence", user.skills[1]);
                        command.Parameters.AddWithValue("@Strength", user.skills[2]);
                        command.Parameters.AddWithValue("@Constitution", user.skills[3]);
                        command.Parameters.AddWithValue("@Ranged", user.skills[4]);
                        command.Parameters.AddWithValue("@Prayer", user.skills[5]);
                        command.Parameters.AddWithValue("@Magic", user.skills[6]);
                        command.Parameters.AddWithValue("@Cooking", user.skills[7]);
                        command.Parameters.AddWithValue("@Woodcutting", user.skills[8]);
                        command.Parameters.AddWithValue("@Fletching", user.skills[9]);
                        command.Parameters.AddWithValue("@Fishing", user.skills[10]);
                        command.Parameters.AddWithValue("@Firemaking", user.skills[11]);
                        command.Parameters.AddWithValue("@Crafting", user.skills[12]);
                        command.Parameters.AddWithValue("@Smithing", user.skills[13]);
                        command.Parameters.AddWithValue("@Mining", user.skills[14]);
                        command.Parameters.AddWithValue("@Herblore", user.skills[15]);
                        command.Parameters.AddWithValue("@Agility", user.skills[16]);
                        command.Parameters.AddWithValue("@Thieving", user.skills[17]);
                        command.Parameters.AddWithValue("@Slayer", user.skills[18]);
                        command.Parameters.AddWithValue("@Farming", user.skills[19]);
                        command.Parameters.AddWithValue("@Runecrafting", user.skills[20]);
                        command.Parameters.AddWithValue("@Hunter", user.skills[21]);
                        command.Parameters.AddWithValue("@Construction", user.skills[22]);
                        command.Parameters.AddWithValue("@Summoning", user.skills[23]);
                        command.Parameters.AddWithValue("@Dungeoneering", user.skills[24]);
                        command.Parameters.AddWithValue("@Divination", user.skills[25]);
                        command.Parameters.AddWithValue("@Invention", user.skills[26]);
                        command.Parameters.AddWithValue("@Overall", user.overallXP);
                        command.Parameters.AddWithValue("@SkillTime", user.skillTime);
                        if (teamID == null) {
                            command.Parameters.AddWithValue("@TeamID", 1);
                        } else{ 
                            command.Parameters.AddWithValue("@TeamID", teamID);
                        }
                        command.Parameters.AddWithValue("@EventID", eventID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
		}
        public static void uppdateEventUser(EventUser user) {
			try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.insertEventUserSQL(), connection))
                    {
                        // The structure of this is because of the ID per skill!!!
                        command.Parameters.AddWithValue("@Username", user.name);
                        command.Parameters.AddWithValue("@eventID", user.eventID);
                        command.Parameters.AddWithValue("@AttackXP", user.skills[0]);
                        command.Parameters.AddWithValue("@DefenceXP", user.skills[1]);
                        command.Parameters.AddWithValue("@StrengthXP", user.skills[2]);
                        command.Parameters.AddWithValue("@ConstitutionXP", user.skills[3]);
                        command.Parameters.AddWithValue("@RangedXP", user.skills[4]);
                        command.Parameters.AddWithValue("@PrayerXP", user.skills[5]);
                        command.Parameters.AddWithValue("@MagicXP", user.skills[6]);
                        command.Parameters.AddWithValue("@CookingXP", user.skills[7]);
                        command.Parameters.AddWithValue("@WoodcuttingXP", user.skills[8]);
                        command.Parameters.AddWithValue("@FletchingXP", user.skills[9]);
                        command.Parameters.AddWithValue("@FishingXP", user.skills[10]);
                        command.Parameters.AddWithValue("@FiremakingXP", user.skills[11]);
                        command.Parameters.AddWithValue("@CraftingXP", user.skills[12]);
                        command.Parameters.AddWithValue("@SmithingXP", user.skills[13]);
                        command.Parameters.AddWithValue("@MiningXP", user.skills[14]);
                        command.Parameters.AddWithValue("@HerbloreXP", user.skills[15]);
                        command.Parameters.AddWithValue("@AgilityXP", user.skills[16]);
                        command.Parameters.AddWithValue("@ThievingXP", user.skills[17]);
                        command.Parameters.AddWithValue("@SlayerXP", user.skills[18]);
                        command.Parameters.AddWithValue("@FarmingXP", user.skills[19]);
                        command.Parameters.AddWithValue("@RunecraftingXP", user.skills[20]);
                        command.Parameters.AddWithValue("@HunterXP", user.skills[21]);
                        command.Parameters.AddWithValue("@ConstructionXP", user.skills[22]);
                        command.Parameters.AddWithValue("@SummoningXP", user.skills[23]);
                        command.Parameters.AddWithValue("@DungeoneeringXP", user.skills[24]);
                        command.Parameters.AddWithValue("@DivinationXP", user.skills[25]);
                        command.Parameters.AddWithValue("@InventionXP", user.skills[26]);
                        command.Parameters.AddWithValue("@OverallXP", user.overallXP);
                        command.Parameters.AddWithValue("@AttackPoints", user.points[0]);
                        command.Parameters.AddWithValue("@DefencePoints", user.points[1]);
                        command.Parameters.AddWithValue("@StrengthPoints", user.points[2]);
                        command.Parameters.AddWithValue("@ConstitutionPoints", user.points[3]);
                        command.Parameters.AddWithValue("@RangedPoints", user.points[4]);
                        command.Parameters.AddWithValue("@PrayerPoints", user.points[5]);
                        command.Parameters.AddWithValue("@MagicPoints", user.points[6]);
                        command.Parameters.AddWithValue("@CookingPoints", user.points[7]);
                        command.Parameters.AddWithValue("@WoodcuttingPoints", user.points[8]);
                        command.Parameters.AddWithValue("@FletchingPoints", user.points[9]);
                        command.Parameters.AddWithValue("@FishingPoints", user.points[10]);
                        command.Parameters.AddWithValue("@FiremakingPoints", user.points[11]);
                        command.Parameters.AddWithValue("@CraftingPoints", user.points[12]);
                        command.Parameters.AddWithValue("@SmithingPoints", user.points[13]);
                        command.Parameters.AddWithValue("@MiningPoints", user.points[14]);
                        command.Parameters.AddWithValue("@HerblorePoints", user.points[15]);
                        command.Parameters.AddWithValue("@AgilityPoints", user.points[16]);
                        command.Parameters.AddWithValue("@ThievingPoints", user.points[17]);
                        command.Parameters.AddWithValue("@SlayerPoints", user.points[18]);
                        command.Parameters.AddWithValue("@FarmingPoints", user.points[19]);
                        command.Parameters.AddWithValue("@RunecraftingPoints", user.points[20]);
                        command.Parameters.AddWithValue("@HunterPoints", user.points[21]);
                        command.Parameters.AddWithValue("@ConstructionPoints", user.points[22]);
                        command.Parameters.AddWithValue("@SummoningPoints", user.points[23]);
                        command.Parameters.AddWithValue("@DungeoneeringPoints", user.points[24]);
                        command.Parameters.AddWithValue("@DivinationPoints", user.points[25]);
                        command.Parameters.AddWithValue("@InventionPoints", user.points[26]);
                        command.Parameters.AddWithValue("@OverallPoints", user.overallPoints);
                        command.Parameters.AddWithValue("@SkillTime", user.skillTime);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
		}
		public static void newClan(string name) {
            Boolean inDatabase = false;
            if (!Program.clanNames.Contains(name)) {
                Program.clanNames.Add(name);
                try 
                { 
                    using (SqlConnection connection = new SqlConnection(Connection.CS()))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(String.newClan(), connection))
                        {
                                command.Parameters.AddWithValue("@name", name);
                                command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    if (!e.ToString().Contains("Unique Clan Name")) {
                        Program.clanNames.Remove(name);
                        inDatabase = true;
                    }
                }
                if(!inDatabase)
                    Program.clans.Add(new Clan(name));
            }
		}
		public static void newEvent(string name, EventTypes eventType, bool isTeamed, DateTime startTime, DateTime endTime, int intervallInMinutes) {
            Console.WriteLine("Adding new event to DB: {0}", name);
            Boolean inDatabase = false;
            if (!Program.clanNames.Contains(name)) {
                Program.clanNames.Add(name);
                try 
                { 
                    using (SqlConnection connection = new SqlConnection(Connection.CS()))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(String.newEvent(), connection))
                        {
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@Type", eventType.ToString());
                                command.Parameters.AddWithValue("@isTeamed", isTeamed);
                                command.Parameters.AddWithValue("@startTime", startTime);
                                command.Parameters.AddWithValue("@endTime", endTime);
                                command.Parameters.AddWithValue("@intervalMinutes", intervallInMinutes);
                                command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    if (!e.ToString().Contains("Unique Clan Name")) {
                        Program.clanNames.Remove(name);
                        inDatabase = true;
                    }
                }
                if(!inDatabase)
                    Program.clans.Add(new Clan(name));
            }
		}
		public static void insertSkill(string name, int eventID, DateTime startTime, DateTime endTime) {
            Console.WriteLine("Adding new skill: {0} for event: {1} to DB", name, eventID);
            if (!Program.clanNames.Contains(name)) {
                Program.clanNames.Add(name);
                try 
                { 
                    using (SqlConnection connection = new SqlConnection(Connection.CS()))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(String.insertSkillSQL(), connection))
                        {
                                command.Parameters.AddWithValue("@SkillName", name);
                                command.Parameters.AddWithValue("@EventID", eventID);
                                command.Parameters.AddWithValue("@StartTime", startTime);
                                command.Parameters.AddWithValue("@EndTime", endTime);
                                command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
		}
    }
}