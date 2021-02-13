using System;
using System.Collections.Generic;

#nullable disable

namespace Lab1ICTP
{
    public partial class Career
    {
        public int CareerId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PositionId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Position Position { get; set; }
        public virtual Team Team { get; set; }
    }
}
