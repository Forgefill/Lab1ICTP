using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class City
    {
        public City()
        {
            Stadia = new HashSet<Stadium>();
            Teams = new HashSet<Team>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stadium> Stadia { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
