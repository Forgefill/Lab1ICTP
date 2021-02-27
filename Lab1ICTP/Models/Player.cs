using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Повне Ім'я")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string FullName { get; set; }


        public virtual ICollection<Career> Careers { get; set; }
    }
}
