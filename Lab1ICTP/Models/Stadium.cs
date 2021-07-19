using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MinLength(5)]
        [MaxLength(25)]
        [Display(Name = "Стадіон")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string Name { get; set; }
        [Display(Name = "Місто")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public int CityId { get; set; }
        [Display(Name = "Місто")]
        public virtual City City { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
