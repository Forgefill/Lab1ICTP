using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Lab1ICTP
{
    public partial class Team
    {
        public Team()
        {
            Careers = new HashSet<Career>();
            GameTeamId2Navigations = new HashSet<Game>();
            GameTeams = new HashSet<Game>();
        }

        public int TeamId { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        public string Name { get; set; }
        [Display(Name = "Місто")]
        public int CityId { get; set; }
        [Display(Name = "Місто")]
        public virtual City City { get; set; }
        public virtual ICollection<Career> Careers { get; set; }
        public virtual ICollection<Game> GameTeamId2Navigations { get; set; }
        public virtual ICollection<Game> GameTeams { get; set; }
    }
}
