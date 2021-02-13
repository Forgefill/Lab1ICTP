using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Player
    {
        public Player()
        {
            Careers = new HashSet<Career>();
        }

        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }

        public virtual ICollection<Career> Careers { get; set; }
    }
}
