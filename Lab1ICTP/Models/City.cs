using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Місто")]
        public string Name { get; set; }

        public virtual ICollection<Stadium> Stadia { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
