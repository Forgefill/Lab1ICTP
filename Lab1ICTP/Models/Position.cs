using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string Name { get; set; }

        public virtual ICollection<Career> Careers { get; set; }
    }
}
