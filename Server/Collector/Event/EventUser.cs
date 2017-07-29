using System;
using Collector;

namespace Event {
    class EventUser : User {
        public int[] points;
        public long overallPoints {get;}
        public int eventID {get;}
        public int teamID {get;}
        public EventUser(string name, string clan, int eventID) : base(name, clan) {
            UpdateFromDB();
            this.eventID = eventID;
        }
        public EventUser(string name, int[] skills, int[] points, long overallXP, long overallPoints, DateTime skillTime) : base(name, skills, overallXP, skillTime) {
            this.points = points;
            this.overallPoints = overallPoints;
        }
        public EventUser(User user, int[] points) : base(user.name, user.skills, user.overallXP, user.skillTime) {
            this.points = points;
        }
        public EventUser(string name, int[] skills, int[] points, long overallXP, long overallPoints, DateTime skillTime, int teamID) : base(name, skills, overallXP, skillTime) {
            this.points = points;
            this.overallPoints = overallPoints;
            this.teamID = teamID;
        }

        public override void UpdateFromDB() {
            EventUser user = Sql.Object.userFromEventUserTable(name, eventID);
            if (user != null)
            {
                this.skills = user.skills;
                this.overallXP = user.overallXP;
                this.skillTime = user.skillTime;
            }
            else {
                base.UpdateFromDB();
            }
        }
    }
}