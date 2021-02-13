using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Stadium
    {
        public Stadium()
        {
            Games = new HashSet<Game>();
        }

        public int StadiumId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
