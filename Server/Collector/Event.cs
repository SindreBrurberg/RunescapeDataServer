namespace Collector {
    class Event {
        EventTypes eventType;
        public Event(EventTypes eventType, string eventName, User[] partisipents) 
        {
            this.eventType = eventType;
            switch (eventType) 
            {
                case EventTypes.Skills:
                    break;
                case EventTypes.DailySkills:
                    break;
            }
        }
    }
    enum EventTypes {
        
        Skills,
        DailySkills
    }
}