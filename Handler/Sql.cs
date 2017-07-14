using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

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
        // The structure of this is because of the ID per skill!!!
        // look at the possibilities to insert the user object here insted of individual values.
		public static void updateUser(string name, int Attack, int Defence, int Strength, int Constitution, int Ranged, int Prayer, int Magic, int Cooking, 
        int Woodcutting, int Fletching, int Fishing, int Firemaking, int Crafting, int Smithing, int Mining, int Herblore, int Agility, int Thieving, int Slayer, 
        int Farming, int Runecrafting, int Construction, int Hunter, int Summoning, int Dungeoneering, int Divination, int Invention, long Overall, string Clan, DateTime SkillTime) {
            int ClanID = 0;
			try 
            { 
                using (SqlConnection connection = new SqlConnection(CS()))
                {
                    connection.Open(); 
                    StringBuilder sbget = new StringBuilder();
                    sbget.Append("SELECT ID ");
                    sbget.Append("FROM [dbo].[Clan] ");
                    sbget.Append("where name=@Name");
                    String sqlGet = sbget.ToString();
                    using (SqlCommand command = new SqlCommand(sqlGet, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Clan);
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
                    cmdCount.Parameters.AddWithValue("@Name", name);
                    int count = (int)cmdCount.ExecuteScalar();
                    String sql;
                    if (count > 0) {
                        insertNewUserTimeFromUser(name);
                        sql = updateUserSQL();
                    } else {
                        sql = insertUserSQL();
                    }
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Attack", Attack);
                        command.Parameters.AddWithValue("@Strength", Strength);
                        command.Parameters.AddWithValue("@Defence", Defence);
                        command.Parameters.AddWithValue("@Ranged", Ranged);
                        command.Parameters.AddWithValue("@Prayer", Prayer);
                        command.Parameters.AddWithValue("@Magic", Magic);
                        command.Parameters.AddWithValue("@Constitution", Constitution);
                        command.Parameters.AddWithValue("@Crafting", Crafting);
                        command.Parameters.AddWithValue("@Mining", Mining);
                        command.Parameters.AddWithValue("@Smithing", Smithing);
                        command.Parameters.AddWithValue("@Fishing", Fishing);
                        command.Parameters.AddWithValue("@Cooking", Cooking);
                        command.Parameters.AddWithValue("@Firemaking", Firemaking);
                        command.Parameters.AddWithValue("@Woodcutting", Woodcutting);
                        command.Parameters.AddWithValue("@Runecrafting", Runecrafting);
                        command.Parameters.AddWithValue("@Dungeoneering", Dungeoneering);
                        command.Parameters.AddWithValue("@Agility", Agility);
                        command.Parameters.AddWithValue("@Herblore", Herblore);
                        command.Parameters.AddWithValue("@Thieving", Thieving);
                        command.Parameters.AddWithValue("@Fletching", Fletching);
                        command.Parameters.AddWithValue("@Slayer", Slayer);
                        command.Parameters.AddWithValue("@Farming", Farming);
                        command.Parameters.AddWithValue("@Construction", Construction);
                        command.Parameters.AddWithValue("@Hunter", Hunter);
                        command.Parameters.AddWithValue("@Summoning", Summoning);
                        command.Parameters.AddWithValue("@Divination", Divination);
                        command.Parameters.AddWithValue("@Invention ", Invention);
                        command.Parameters.AddWithValue("@Overall", Overall);
                        command.Parameters.AddWithValue("@ClanId", ClanID);
                        command.Parameters.AddWithValue("@SkillTime", SkillTime);
                        command.ExecuteNonQuery();
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
                    StringBuilder sbGet = new StringBuilder();
                    sbGet.Append("SELECT TOP 1 * ");
                    sbGet.Append("FROM [dbo].[User] ");
                    sbGet.Append("WHERE Name = 'FatMine'");
                    String sqlGet = sbGet.ToString();
                    StringBuilder sbGetTime = new StringBuilder();
                    sbGetTime.Append("SELECT TOP 1 * ");
                    sbGetTime.Append("FROM [dbo].[UserTime] ");
                    sbGetTime.Append("WHERE Username = 'FatMine'");
                    String sqlGetTime = sbGetTime.ToString();
                    using (SqlCommand commandGet = new SqlCommand(sqlGet, connectionGet))
                    {
                        Console.WriteLine(name);
                        commandGet.Parameters.AddWithValue("@Name", name);
                        using (SqlDataReader reader = commandGet.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("reading");
                                using (SqlConnection connection = new SqlConnection(CS()))
                                {
                                    connection.Open();
                                    Boolean isNew = false;
                                    using (SqlCommand commandGetTime = new SqlCommand(sqlGetTime, connection))
                                    {
                                        commandGetTime.Parameters.AddWithValue("@Username", name);
                                        using (SqlDataReader readerTime = commandGetTime.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                if (reader["SkillTime"] != readerTime["SkillTime"])
                                                    isNew = true;
                                            }
                                        }
                                    }
                                    if (isNew) {
                                        Console.WriteLine("New Time");
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