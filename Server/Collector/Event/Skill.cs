using Collector;
using System.Collections.Generic;

namespace Event {
    class Skill : Event { 
        public Skill (string name, List<Collector.TimedSkill> skills, EventUser[] users) : base(name, users) {
            
        }
        public Skill (string name, List<Collector.TimedSkill> skills, Team[] teams) : base(name, teams) {
            
        }

        public void update() {
            
        }
    }
}