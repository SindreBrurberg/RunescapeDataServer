using System.Text;

namespace Sql {
    class String {
        public static string updateUserSQL() {
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
        public static string insertUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[User] ([Name], [Attack], [Strength], [Defence], [Ranged], [Prayer], [Magic], [Constitution], [Crafting], [Mining], [Smithing], [Fishing]");
            sb.Append(", [Cooking], [Firemaking], [Woodcutting], [Runecrafting], [Dungeoneering], [Agility], [Herblore], [Thieving], [Fletching], [Slayer], [Farming]");
            sb.Append(", [Construction], [Hunter], [Summoning], [Divination], [Invention], [Overall], [ClanID], [SkillTime])");
            sb.Append("VALUES (@Name, @Attack, @Strength, @Defence, @Ranged, @Prayer, @Magic, @Constitution, @Crafting, @Mining, @Smithing, @Fishing, @Cooking, @Firemaking");
            sb.Append(", @Woodcutting, @Runecrafting, @Dungeoneering, @Agility, @Herblore, @Thieving, @Fletching, @Slayer, @Farming, @Construction, @Hunter, @Summoning, @Divination");
            sb.Append(", @Invention, @Overall, @ClanID, @SkillTime);");
            return sb.ToString();
        }
        public static string insertUserTimeSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[UserTime] ([Username], [Attack], [Strength], [Defence], [Ranged], [Prayer], [Magic], [Constitution], [Crafting], [Mining], [Smithing], [Fishing]");
            sb.Append(", [Cooking], [Firemaking], [Woodcutting], [Runecrafting], [Dungeoneering], [Agility], [Herblore], [Thieving], [Fletching], [Slayer], [Farming]");
            sb.Append(", [Construction], [Hunter], [Summoning], [Divination], [Invention], [Overall], [SkillTime])");
            sb.Append("VALUES (@Username, @Attack, @Strength, @Defence, @Ranged, @Prayer, @Magic, @Constitution, @Crafting, @Mining, @Smithing, @Fishing, @Cooking, @Firemaking");
            sb.Append(", @Woodcutting, @Runecrafting, @Dungeoneering, @Agility, @Herblore, @Thieving, @Fletching, @Slayer, @Farming, @Construction, @Hunter, @Summoning, @Divination");
            sb.Append(", @Invention, @Overall, @SkillTime);");
            return sb.ToString();
        }
        public static string getUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[User] ");
            sb.Append("WHERE Name = @Name");
            return sb.ToString();
        }
        public static string getUserTimeNewestSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[UserTime] ");
            sb.Append("WHERE Username = @Username ");
            sb.Append("ORDER by id DESC");
            return sb.ToString();
        }
        public static string getClanIDFromClanNameSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ID ");
            sb.Append("FROM [dbo].[Clan] ");
            sb.Append("WHERE name=@Name");
            return sb.ToString();
        }
        public static string getClanNameFromClanIDSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Name ");
            sb.Append("FROM [dbo].[Clan] ");
            sb.Append("WHERE ID=@ID");
            return sb.ToString();
        }
        public static string newClan() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Clan] (name) ");
            sb.Append("VALUES (@name);");
            return sb.ToString();
        }
    }
}