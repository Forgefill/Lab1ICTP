using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Game
    {
        public int GameId { get; set; }
        public int? StartTime { get; set; }
        public int StadiumId { get; set; }
        public int? TournamentId { get; set; }
        public int TeamId { get; set; }
        public int TeamId2 { get; set; }
        public int RefereeId { get; set; }

        public virtual Referee Referee { get; set; }
        public virtual Stadium Stadium { get; set; }
        public virtual Team Team { get; set; }
        public virtual Team TeamId2Navigation { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
