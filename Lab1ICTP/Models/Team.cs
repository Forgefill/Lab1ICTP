using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Team
    {
        public Team()
        {
            Careers = new HashSet<Career>();
            GameTeamId2Navigations = new HashSet<Game>();
            GameTeams = new HashSet<Game>();
        }

        public int TeamId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Career> Careers { get; set; }
        public virtual ICollection<Game> GameTeamId2Navigations { get; set; }
        public virtual ICollection<Game> GameTeams { get; set; }
    }
}
