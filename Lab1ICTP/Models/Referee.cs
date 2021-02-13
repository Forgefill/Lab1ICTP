using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Referee
    {
        public Referee()
        {
            Games = new HashSet<Game>();
        }

        public int RefereeId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
