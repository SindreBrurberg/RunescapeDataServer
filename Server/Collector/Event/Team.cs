namespace Event {
    class Team {
        string name;
        EventUser[] users;
        public Team(string name, EventUser[] users) {
            this.name = name;
            this.users = users;
        }
        public long points() {
            long points = 0;
            foreach (EventUser user in users) {
                points += user.overallPoints;
            }
            return points;
        }
    }
}