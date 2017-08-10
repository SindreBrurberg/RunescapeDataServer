using System;

namespace Collector {
    class TimedSkill {
        public Skill skill {get; private set;}
        public DateTime start {get; private set;}
        public DateTime end {get; private set;}
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