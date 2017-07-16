using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System;
using Collector;

namespace Sql {
    class Object {
        public static List<string> clans() {
            List<string> clans = new List<string>();
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
        
        public static User newUserFromUserTable(string Username) {
            using (SqlConnection connection = new SqlConnection(Connection.CS()))
            {
                connection.Open();  
                int[] skills = new int[27];
                long overallXP = 0;
                int ClanID = 0;
                string Clan = "";
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
                            Clan = reader["Name"].ToString();
                        }
                    }
                } 
                return new User(Username, skills, overallXP, skillTime);
            }
            return null;
        }
    }
}