using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Position
    {
        public Position()
        {
            Careers = new HashSet<Career>();
        }

        public int PositionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Career> Careers { get; set; }
    }
}
