using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name ="Повне Ім'я")]
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        public string FullName { get; set; }
        [Display(Name = "Дата народження")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
