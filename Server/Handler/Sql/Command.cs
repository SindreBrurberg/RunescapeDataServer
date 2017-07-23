using System;
using System.Data.SqlClient;
using Server;
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
            int ClanID = 0;
			try 
            { 
                using (SqlConnection connection = new SqlConnection(Connection.CS()))
                {
                    connection.Open(); 
                    
                    using (SqlCommand command = new SqlCommand(String.getClanIDFromClanNameSQL(), connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.clan);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								ClanID = Int32.Parse(reader["ID"].ToString());
                            }
                        }
                    } 
                    if (ClanID == 0) {
                        Console.WriteLine("Invalid Clan");
                        return;
                    }
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
                                        runSQLQuerry(String.updateUserSQL(), user, ClanID);
                                    }     
                                }
                            }
                        }
                    } else {
                        runSQLQuerry(String.insertUserSQL(), user, ClanID);
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
                User user = Object.newUserFromUserTable(name);
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
		public static void NewClan(string name) {
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
    }
}