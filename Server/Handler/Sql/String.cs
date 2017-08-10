using System.Text;
using System;

namespace Sql {
    class String {
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
        public static string insertNewEventUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[EventUser] ([Name], [EventID], [Attack], [Strength], [Defence], [Ranged], [Prayer], [Magic], [Constitution], [Crafting]");
            sb.Append(", [Mining], [Smithing], [Fishing], [Cooking], [Firemaking], [Woodcutting], [Runecrafting], [Dungeoneering], [Agility], [Herblore]");
            sb.Append(", [Thieving], [Fletching], [Slayer], [Farming], [Construction], [Hunter], [Summoning], [Divination], [Invention], [Overall], [TeamID], [SkillTime])");
            sb.Append("VALUES (@Name, @EventID, @Attack, @Strength, @Defence, @Ranged, @Prayer, @Magic, @Constitution, @Crafting, @Mining, @Smithing");
            sb.Append(", @Fishing, @Cooking, @Firemaking, @Woodcutting, @Runecrafting, @Dungeoneering, @Agility, @Herblore, @Thieving, @Fletching, @Slayer");
            sb.Append(", @Farming, @Construction, @Hunter, @Summoning, @Divination, @Invention, @Overall, @TeamID, @SkillTime);");
            return sb.ToString();
        }
        public static string insertEventUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[EventUser] ([Name], [EventID], [AttackXP], [StrengthXP], [DefenceXP], [RangedXP], [PrayerXP], [MagicXP], [ConstitutionXP], [CraftingXP]");
            sb.Append(", [MiningXP], [SmithingXP], [FishingXP], [CookingXP], [FiremakingXP], [WoodcuttingXP], [RunecraftingXP], [DungeoneeringXP], [AgilityXP], [HerbloreXP]");
            sb.Append(", [ThievingXP], [FletchingXP], [SlayerXP], [FarmingXP], [ConstructionXP], [HunterXP], [SummoningXP], [DivinationXP], [InventionXP], [OverallXP]");
            sb.Append(", [AttackPoints], [StrengthPoints], [DefencePoints], [RangedPoints], [PrayerPoints], [MagicPoints], [ConstitutionPoints]");
            sb.Append(", [CraftingPoints], [MiningPoints], [SmithingPoints], [FishingPoints], [CookingPoints], [FiremakingPoints], [WoodcuttingPoints], [RunecraftingPoints]");
            sb.Append(", [DungeoneeringPoints], [AgilityPoints], [HerblorePoints], [ThievingPoints], [FletchingPoints], [SlayerPoints], [FarmingPoints], [ConstructionPoints]");
            sb.Append(", [HunterPoints], [SummoningPoints], [DivinationPoints], [InventionPoints], [OverallPoints], [TeamID], [SkillTime])");
            sb.Append("VALUES (@Username, @EventID, @AttackXP, @StrengthXP, @DefenceXP, @RangedXP, @PrayerXP, @MagicXP, @ConstitutionXP, @CraftingXP, @MiningXP, @SmithingXP");
            sb.Append(", @FishingXP, @CookingXP, @FiremakingXP, @WoodcuttingXP, @RunecraftingXP, @DungeoneeringXP, @AgilityXP, @HerbloreXP, @ThievingXP, @FletchingXP, @SlayerXP");
            sb.Append(", @FarmingXP, @ConstructionXP, @HunterXP, @SummoningXP, @DivinationXP, @InventionXP, @OverallXP");
            sb.Append(", @AttackPoints, @StrengthPoints, @DefencePoints, @RangedPoints, @PrayerPoints, @MagicPoints, @ConstitutionPoints, @CraftingPoints, @MiningPoints");
            sb.Append(", @SmithingPoints, @FishingPoints, @CookingPoints, @FiremakingPoints, @WoodcuttingPoints, @RunecraftingPoints, @DungeoneeringPoints, @AgilityPoints");
            sb.Append(", @HerblorePoints, @ThievingPoints, @FletchingPoints, @SlayerPoints, @FarmingPoints, @ConstructionPoints, @HunterPoints, @SummoningPoints, @DivinationPoints");
            sb.Append(", @InventionPoints, @OverallPoints, @TeamID, @SkillTime);");
            return sb.ToString();
        }
        public static string updateEventUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE [dbo].[EventUser] SET [AttackXP] = @AttackXP, [StrengthXP] = @StrengthXP, [DefenceXP] = @DefenceXP, [RangedXP] = @RangedXP");
            sb.Append(", [PrayerXP] = @PrayerXP, [MagicXP] = @MagicXP, [ConstitutionXP] = @ConstitutionXP, [CraftingXP] = @CraftingXP, [MiningXP] = @MiningXP");
            sb.Append(", [SmithingXP] = @SmithingXP, [FishingXP] = @FishingXP, [CookingXP] = @CookingXP, [FiremakingXP] = @FiremakingXP, [WoodcuttingXP] = @WoodcuttingXP");
            sb.Append(", [RunecraftingXP] = @RunecraftingXP, [AgilityXP] = @AgilityXP, [HerbloreXP] = @HerbloreXP, [ThievingXP] = @ThievingXP, [FletchingXP] = @FletchingXP");
            sb.Append(", [SlayerXP] = @SlayerXP, [FarmingXP] = @FarmingXP, [ConstructionXP] = @ConstructionXP, [HunterXP] = @HunterXP, [SummoningXP] = @SummoningXP");
            sb.Append(", [DivinationXP] = @DivinationXP, [InventionXP] = @InventionXP, [OverallXP] = @OverallXP");
            sb.Append(", [AttackPoints] = @AttackPoints, [StrengthPoints] = @StrengthPoints, [DefencePoints] = @DefencePoints, [RangedPoints] = @RangedPoints");
            sb.Append(", [PrayerPoints] = @PrayerPoints, [MagicPoints] = @MagicPoints, [ConstitutionPoints] = @ConstitutionPoints, [CraftingPoints] = @Crafting");
            sb.Append(", [MiningPoints] = @MiningPoints, [SmithingPoints] = @SmithingPoints, [FishingPoints] = @FishingPoints, [CookingPoints] = @CookingPoints");
            sb.Append(", [FiremakingPoints] = @FiremakingPoints, [WoodcuttingPoints] = @WoodcuttingPoints, [RunecraftingPoints] = @RunecraftingPoints, [AgilityPoints] = @AgilityPoints");
            sb.Append(", [HerblorePoints] = @HerblorePoints, [ThievingPoints] = @ThievingPoints, [FletchingPoints] = @FletchingPoints, [SlayerPoints] = @SlayerPoints");
            sb.Append(", [FarmingPoints] = @FarmingPoints, [ConstructionPoints] = @ConstructionPoints, [HunterPoints] = @HunterPoints, [SummoningPoints] = @SummoningPoints");
            sb.Append(", [DivinationPoints] = @DivinationPoints, [InventionPoints] = @InventionPoints, [OverallPoints] = @OverallPoints, [SkillTime] = @SkillTime ");
            sb.Append("WHERE Name = @Name AND eventID = @eventID");
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
        public static string insertSkillSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Skill] ([EventID], [SkillName], [StartTime], [EndTime])");
            sb.Append("VALUES (@EventID, @SkillName, @StartTime, @EndTime);");
            return sb.ToString();
        }
        public static string getUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[User] ");
            sb.Append("WHERE Name = @Name");
            return sb.ToString();
        }
        public static string getUsersInClanSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[User] ");
            sb.Append("WHERE clanID = @clanID");
            return sb.ToString();
        }
        public static string getEventUserSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 * ");
            sb.Append("FROM [dbo].[EventUser] ");
            sb.Append("WHERE Username = @Name AND eventID = @eventID");
            return sb.ToString();
        }
        public static string getEventUsersSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[EventUser] ");
            sb.Append("WHERE eventID = @eventID");
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
        public static string getEventFromNameSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[Event] ");
            sb.Append("WHERE Name=@Name");
            return sb.ToString();
        }
        public static string newEvent() {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Event] ([Name], [Type], [isTeamed], [startTime], [endTime], [intervalMinutes]) ");
            sb.Append("VALUES (@Name, @Type, @isTeamed, @startTime, @endTime, @intervalMinutes);");
            return sb.ToString();
        }
        public static string eventsNotEnded() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[Event] ");
            sb.Append("WHERE EndTime > '" + DateTime.Now + "'");
            return sb.ToString();
        }
        public static string skillsInEventSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM [dbo].[Skill] ");
            sb.Append("WHERE EventID = @EventID");
            return sb.ToString();
        }
        public static string getTeamNameFromIDSQL() {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Name ");
            sb.Append("FROM [dbo].[Team] ");
            sb.Append("WHERE ID = @ID");
            return sb.ToString();
        }
    }
}