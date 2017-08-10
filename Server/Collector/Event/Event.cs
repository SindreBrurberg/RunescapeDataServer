using Collector;
using System;

namespace Event {
    class Event {
        public string name{get; private set;}
        public EventUser[] users{get; private set;}
        // public Team[] teams{get; private set;}
        public int eventID{get; private set;}
        public EventTypes type{get; private set;}
        public DateTime startTime{get; private set;}
        public DateTime endTime{get; private set;}
        public int intervall{get; private set;}
        public Event (string name, int eventID, EventTypes type, EventUser[] users, DateTime startTime, DateTime endTime, int intervall) {
            this.name = name;
            this.eventID = eventID;
            this.type = type;
            this.users = users;
        }
        // public Event (string name, int eventID, EventTypes type, Team[] teams, DateTime startTime, DateTime endTime, int intervall) {
        //     this.name = name;
        //     this.eventID = eventID;
        //     this.type = type;
        //     this.teams = teams;
        // }
        protected int milliFromNow(DateTime time) {
            return (int)(time.Ticks - DateTime.Now.Ticks / 10000);
        }

        public void update() {
            
        }
    }
}