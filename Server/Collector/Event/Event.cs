using Collector;

namespace Event {
    class Event {
        string name;
        EventUser[] users;
        Team[] teams;
        int eventID;
        public Event (string name, EventUser[] users) {
            this.name = name;
            this.users = users;
        }
        public Event (string name, int eventID, EventUser[] users) {
            this.name = name;
            this.eventID = eventID;
            this.users = users;
        }
        public Event (string name, Team[] teams) {
            this.name = name;
            this.teams = teams;
        }
        public Event (string name, int eventID, Team[] teams) {
            this.name = name;
            this.eventID = eventID;
            this.teams = teams;
        }
    }
}