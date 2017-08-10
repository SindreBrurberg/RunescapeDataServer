using Collector;
using System.Collections.Generic;
using System;
using Sql;

namespace Event {
    class EventHandler {
        static List<Event> events = new List<Event>();
        public static void initEvent(EventTypes eventType, string eventName, List<TimedSkill> skills, EventUser[] partisipents, DateTime startTime, DateTime endTime, int intervallInMinutes) 
        {
            Command.newEvent(eventName, eventType, false, startTime, endTime, intervallInMinutes);
            int eventID = Sql.Object.eventIDFromName(eventName);
            Console.WriteLine("EventID: {0}", eventID);
            switch (eventType) 
            {
                case EventTypes.Skills:
                    events.Add(new Skill(eventName, eventID, skills, partisipents, startTime, endTime, intervallInMinutes));
                    break;
                case EventTypes.DailySkills:
                    break;
            }
            Console.WriteLine("Partisipents length: {0}", partisipents.Length);
            foreach (EventUser partisipent in partisipents) {
                Command.insertNewEventUserFromUser(partisipent.name, eventID, null);
            }
            foreach (TimedSkill skill in skills) {
                Command.insertSkill(skill.skill.ToString(), eventID, skill.start, skill.end);
            }
        }
        public static void initEvent(EventTypes eventType, string eventName, List<TimedSkill> skills, EventUser[] partisipents, DateTime startTime, DateTime endTime) 
        {
            initEvent(eventType, eventName, skills, partisipents, startTime, endTime, 30);
        }
        public static void initEvent(EventTypes eventType, string eventName, List<TimedSkill> skills, User[] partisipents, DateTime startTime, DateTime endTime) 
        {
            var eventPartisipents = new List<EventUser>();
            foreach (User user in partisipents) {
                eventPartisipents.Add(new EventUser(user, new int[26]));
            }
            initEvent(eventType, eventName, skills, eventPartisipents.ToArray(), startTime, endTime, 30);
        }
        public static void initEvent(EventTypes eventType, string eventName, List<TimedSkill> skills, Team[] teams, DateTime startTime, DateTime endTime, int intervallInMinutes) 
        {
            Command.newEvent(eventName, eventType, true, startTime, endTime, intervallInMinutes);
            int eventID = Sql.Object.eventIDFromName(eventName);
            Console.WriteLine("EventID: {0}", eventID);
            switch (eventType) 
            {
                case EventTypes.Skills:
                    // events.Add(new Skill(eventName, eventID, skills, teams, startTime, endTime, intervallInMinutes));
                    break;
                case EventTypes.DailySkills:
                    break;
            }
        }
        public static void initEvent(EventTypes eventType, string eventName, List<TimedSkill> skills, Team[] teams, DateTime startTime, DateTime endTime) 
        {
            initEvent(eventType, eventName, skills, teams, startTime, endTime, 30);
        }
    }
    enum EventTypes {
        Skills,
        DailySkills
    }
}