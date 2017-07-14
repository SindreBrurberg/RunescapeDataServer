using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Collector;

namespace Handler {
    class Sql {
        public static string Username {private get; set;}
        public static string Password {private get; set;}
        public static string DataSource {private get; set;}
        public static string Catalog {private get; set;}
        private static string CS() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = DataSource; 
                builder.UserID = Username;            
                builder.Password = Password;     
                builder.InitialCatalog = Catalog;
			return builder.ConnectionString;
		}
        public static List<string> clans() {
            List<string> clans = new List<string>();
            try 
            { 
                using (SqlConnection connection = new SqlConnection(CS()))
                {
                    connection.Open();       
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT [Name]");
                    sb.Append("FROM [dbo].[Clan] ");
                    String sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
								clans.Add(reader["Name"].ToString());
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
        private static string updateUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE [dbo].[User] SET  [Attack] = @Attack, [Strength] = @Strength, [Defence] = @Defence, [Ranged] = @Ranged, ");
            sb.Append("[Prayer] = @Prayer, [Magic] = @Magic, [Constitution] = @Constitution, [Crafting] = @Crafting, [Mining] = @Mining, ");
            sb.Append("[Smithing] = @Smithing, [Fishing] = @Fishing, [Cooking] = @Cooking, [Firemaking] = @Firemaking, [Woodcutting] = @Woodcutting, ");
            sb.Append("[Runecrafting] = @Runecrafting, [Agility] = @Agility, [Herblore] = @Herblore, [Thieving] = @Thieving, [Fletching] = @Fletching, ");
            sb.Append("[Slayer] = @Slayer, [Farming] = @Farming, [Construction] = @Construction, [Hunter] = @Hunter, [Summoning] = @Summoning, ");
            sb.Append("[Divination] = @Divination, [Invention] = @Invention, [Overall] = @Overall, [ClanID] = @ClanID, [SkillTime] = @SkillTime ");
            sb.Append("WHERE name = @name");
            return sb.ToString();
        }
        private static string insertUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[User] ([Name], [Attack], [Strength], [Defence], [Ranged], [Prayer], [Magic], [Constitution], [Crafting], [Mining], [Smithing], [Fishing]");
            sb.Append(", [Cooking], [Firemaking], [Woodcutting], [Runecrafting], [Dungeoneering], [Agility], [Herblore], [Thieving], [Fletching], [Slayer], [Farming]");
            sb.Append(", [Construction], [Hunter], [Summoning], [Divination], [Invention], [Overall], [ClanID], [SkillTime])");
            sb.Append("VALUES (@Name, @Attack, @Strength, @Defence, @Ranged, @Prayer, @Magic, @Constitution, @Crafting, @Mining, @Smithing, @Fishing, @Cooking, @Firemaking");
            sb.Append(", @Woodcutting, @Runecrafting, @Dungeoneering, @Agility, @Herblore, @Thieving, @Fletching, @Slayer, @Farming, @Construction, @Hunter, @Summoning, @Divination");
            sb.Append(", @Invention, @Overall, @ClanID, @SkillTime);");
            return sb.ToString();
        }
        private static string insertUserTimeSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[UserTime] ([Username], [Attack], [Strength], [Defence], [Ranged], [Prayer], [Magic], [Constitution], [Crafting], [Mining], [Smithing], [Fishing]");
            sb.Append(", [Cooking], [Firemaking], [Woodcutting], [Runecrafting], [Dungeoneering], [Agility], [Herblore], [Thieving], [Fletching], [Slayer], [Farming]");
            sb.Append(", [Construction], [Hunter], [Summoning], [Divination], [Invention], [Overall], [SkillTime])");
            sb.Append("VALUES (@Username, @Attack, @Strength, @Defence, @Ranged, @Prayer, @Magic, @Constitution, @Crafting, @Mining, @Smithing, @Fishing, @Cooking, @Firemaking");
            sb.Append(", @Woodcutting, @Runecrafting, @Dungeoneering, @Agility, @Herblore, @Thieving, @Fletching, @Slayer, @Farming, @Construction, @Hunter, @Summoning, @Divination");
            sb.Append(", @Invention, @Overall, @SkillTime);");
            return sb.ToString();
        }
        private static string getUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[User] ");
            sb.Append("WHERE Name = @Name");
            return sb.ToString();
        }
        private static string getUserTimeNewestSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[UserTime] ");
            sb.Append("WHERE Username = @Username ");
            sb.Append("ORDER by id DESC");
            return sb.ToString();
        }
        private static string getClanIDFromClanNameSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ID ");
            sb.Append("FROM [dbo].[Clan] ");
            sb.Append("where name=@Name");
            return sb.ToString();
        }
		public static void updateUser(User user) {
            if (user.name.Equals("FatMine")) {
                Console.WriteLine(
                @"CRISES
                USER IS FATMINE
                OMG!!
                READ THIS
                CRISIS
                OMG!!
                !!!
                !!!
                !!!
                !!!");
            }
            int ClanID = 0;
			try 
            { 
                using (SqlConnection connection = new SqlConnection(CS()))
                {
                    connection.Open(); 
                    
                    using (SqlCommand command = new SqlCommand(getClanIDFromClanNameSQL(), connection))
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
                    String sql;
                    Boolean isChanged = true;
                    if (count > 0) {
                        insertNewUserTimeFromUser(user.name);
                        sql = updateUserSQL();
                        using (SqlCommand command = new SqlCommand(getUserSQL(), connection)){
                            command.Parameters.AddWithValue("@Name", user.name);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read()){
                                    if (reader["Overall"].ToString().Equals(user.overallXP.ToString())) 
                                        isChanged = false;
                                }
                            }
                        }
                    } else {
                        sql = insertUserSQL();
                    }
                    if (isChanged) {
                        Console.WriteLine("{0} is updated or new, updating the database", user.name);
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
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
                            command.Parameters.AddWithValue("@Construction", user.skills[21]);
                            command.Parameters.AddWithValue("@Hunter", user.skills[22]);
                            command.Parameters.AddWithValue("@Summoning", user.skills[23]);
                            command.Parameters.AddWithValue("@Dungeoneering", user.skills[24]);
                            command.Parameters.AddWithValue("@Divination", user.skills[25]);
                            command.Parameters.AddWithValue("@Invention ", user.skills[26]);
                            command.Parameters.AddWithValue("@Overall", user.overallXP);
                            command.Parameters.AddWithValue("@ClanId", ClanID);
                            command.Parameters.AddWithValue("@SkillTime", user.skillTime);
                            command.ExecuteNonQuery();
                        }     
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
                using (SqlConnection connectionGet = new SqlConnection(CS()))
                {
                    connectionGet.Open();       
                    using (SqlCommand commandGet = new SqlCommand(getUserSQL(), connectionGet))
                    {
                        commandGet.Parameters.AddWithValue("@Name", name);
                        using (SqlDataReader reader = commandGet.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                using (SqlConnection connection = new SqlConnection(CS()))
                                {
                                    connection.Open();
                                    Boolean isNew = false;
                                    using (SqlCommand commandGetTime = new SqlCommand(getUserTimeNewestSQL(), connection))
                                    {
                                        commandGetTime.Parameters.AddWithValue("@Username", name);
                                        using (SqlDataReader readerTime = commandGetTime.ExecuteReader())
                                        {
                                            Console.WriteLine("Reading information about {0} from user table", name);
                                            Boolean reading = false;
                                            while (readerTime.Read())
                                            {
                                                reading = true;
                                                if (!reader["SkillTime"].ToString().Equals(readerTime["SkillTime"].ToString()))
                                                    isNew = true;
                                            }
                                            if (!reading)
                                                isNew = true;
                                        }
                                    }
                                    if (isNew) {
                                        using (SqlCommand commandInsert = new SqlCommand(insertUserTimeSQL(), connection))
                                        {
                                            commandInsert.Parameters.AddWithValue("@Username", reader["Name"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Attack", reader["Attack"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Strength", reader["Strength"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Defence", reader["Defence"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Ranged", reader["Ranged"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Prayer", reader["Prayer"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Magic", reader["Magic"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Constitution", reader["Constitution"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Crafting", reader["Crafting"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Mining", reader["Mining"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Smithing", reader["Smithing"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Fishing", reader["Fishing"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Cooking", reader["Cooking"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Firemaking", reader["Firemaking"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Woodcutting", reader["Woodcutting"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Runecrafting", reader["Runecrafting"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Dungeoneering", reader["Dungeoneering"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Agility", reader["Agility"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Herblore", reader["Herblore"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Thieving", reader["Thieving"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Fletching", reader["Fletching"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Slayer", reader["Slayer"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Farming", reader["Farming"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Construction", reader["Construction"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Hunter", reader["Hunter"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Summoning", reader["Summoning"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Divination", reader["Divination"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Invention ", reader["Invention"].ToString());
                                            commandInsert.Parameters.AddWithValue("@Overall", reader["Overall"].ToString());
                                            commandInsert.Parameters.AddWithValue("@SkillTime", reader["SkillTime"].ToString());
                                            commandInsert.ExecuteNonQuery();
                                        }
                                    }
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
		}
    }
}