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
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string FirstName { get; set; }
        [Display(Name = "Фамілія")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string SurName { get; set; }

        public virtual ICollection<Career> Careers { get; set; }
    }
}
