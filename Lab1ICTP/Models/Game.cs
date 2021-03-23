using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Lab1ICTP
{
    public partial class Game
    {
        public int GameId { get; set; }
        [Required(ErrorMessage = "Час початку відсутній")]
        [Display(Name = "Час початку")]
        
        public DateTime? StartTime { get; set; }
        [Display(Name = "Стадіон")]
        public int StadiumId { get; set; }
        [Display(Name = "Турнір")]
        public int? TournamentId { get; set; }
        [Display(Name = "Перша Команда")]
        public int TeamId { get; set; }
        [Display(Name = "Друга Команда")]
        public int TeamId2 { get; set; }
        [Display(Name = "Суддя")]
        public int RefereeId { get; set; }
        [Display(Name = "Суддя")]
        public virtual Referee Referee { get; set; }
        [Display(Name = "Стадіон")]
        public virtual Stadium Stadium { get; set; }
        [Display(Name = "Перша Команда")]
        public virtual Team Team { get; set; }
        [Display(Name = "Друга Команда")]
        public virtual Team TeamId2Navigation { get; set; }
        [Display(Name = "Турнір")]
        public virtual Tournament Tournament { get; set; }
    }
}
