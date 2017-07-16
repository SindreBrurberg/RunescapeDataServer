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
        private static void runSQLQuerry(string sql, User user, int ClanID) {
            using (SqlConnection connection = new SqlConnection(CS())) 
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // The structure of this is because of the ID per skill!!!
                    command.Parameters.AddWithValue(ClanID == 0 ? "@Name" : "@Username", user.name);
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
                    if (ClanID == 0)
                        command.Parameters.AddWithValue("@ClanId", ClanID);
                    command.Parameters.AddWithValue("@SkillTime", user.skillTime);
                    command.ExecuteNonQuery();
                }
            }
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
                    if (count > 0) {
                        insertNewUserTimeFromUser(user.name);
                        using (SqlCommand commandGet = new SqlCommand(getUserSQL(), connection)){
                            commandGet.Parameters.AddWithValue("@Name", user.name);
                            using (SqlDataReader reader = commandGet.ExecuteReader())
                            {
                                while (reader.Read()){
                                    if (Int64.Parse(reader["Overall"].ToString()) >= Int64.Parse(user.overallXP.ToString())) 
                                    {
                                        runSQLQuerry(updateUserSQL(), user, ClanID);
                                    }     
                                }
                            }
                        }
                    } else {
                        runSQLQuerry(insertUserSQL(), user, ClanID);
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
                User user = newUserFromUserTable(name);
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
                                if (!user.skillTime.Equals(readerTime["SkillTime"]))
                                    isNew = true;
                            }
                            if (!reading)
                                isNew = true;
                        }
                    }
                    if (isNew) {
                        runSQLQuerry(insertUserTimeSQL(), user, 0);
                        using (SqlCommand commandInsert = new SqlCommand(insertUserTimeSQL(), connection))
                        {
                            commandInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
		}
        public static User newUserFromUserTable(string Username) {
            using (SqlConnection connectionGet = new SqlConnection(CS()))
            {
                connectionGet.Open();       
                using (SqlCommand commandGet = new SqlCommand(getUserSQL(), connectionGet))
                {
                    commandGet.Parameters.AddWithValue("@Name", Username);
                    using (SqlDataReader reader = commandGet.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int[] skills = new int[27];
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
                            return new User(reader["Name"].ToString(), skills, Int64.Parse(reader["Overall"].ToString()), DateTime.Parse(reader["SkillTime"].ToString()));
                        }
                    }
                }
            }
            return null;
        }
    }
}