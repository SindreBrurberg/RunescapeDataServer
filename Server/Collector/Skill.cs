using System;

namespace Collector {
    class TimedSkill {
        Skill skill;
        DateTime start;
        DateTime end;
        public TimedSkill(Skill skill, DateTime start, DateTime end) {
            this.skill = skill;
            this.start = start;
            this.end = end;
        }
    }
    enum Skill {
        Attack,
        Defence,
        Strength,
        Constitution,
        Ranged,
        Prayer,
        Magic,
        Cooking,
        Woodcutting,
        Fletching,
        Fishing,
        Firemaking,
        Crafting,
        Smithing,
        Mining,
        Herblore,
        Agility,
        Thieving,
        Slayer,
        Farming,
        Runecrafting,
        Hunter,
        Construction,
        Summoning,
        Dungeoneering,
        Divination,
        Invention,
        Overall
    }
}