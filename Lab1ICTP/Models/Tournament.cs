using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Tournament
    {
        public Tournament()
        {
            Games = new HashSet<Game>();
        }

        public int TournamentId { get; set; }
        public string Name { get; set; }
        public decimal? Prize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
