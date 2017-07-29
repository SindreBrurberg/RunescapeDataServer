using Collector;

namespace Event {
    class Skill : Event { 
        public Skill (string name, EventUser[] users) : base(name, users) {
            
        }
        public Skill (string name, Team[] teams) : base(name, teams) {
            
        }
    }
}