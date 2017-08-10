using Collector;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Event {
    class Skill : Event { 
        public List<Collector.TimedSkill> skills{get; private set;}


        public Skill (string name, int eventID, List<Collector.TimedSkill> skills, EventUser[] users, 
            DateTime startTime, DateTime endTime, int intervall)
            : base(name, eventID, EventTypes.Skills, users, startTime, endTime, intervall) 
        {
            initUpdate();
        }
        // public Skill (string name, int eventID, List<Collector.TimedSkill> skills, Team[] teams, 
        //     DateTime startTime, DateTime endTime, int intervall)
        //     : base(name, eventID, EventTypes.Skills, teams, startTime, endTime, intervall) 
        // {
        //     initUpdate();
        // }
        private void initUpdate() {
            new Timer(update, null, milliFromNow(startTime), intervall*60*1000);
        }

        public void update(object stateInfo) {
            if (DateTime.Now < endTime) {
                base.update();

            } else {

            }         
        }
    }
}