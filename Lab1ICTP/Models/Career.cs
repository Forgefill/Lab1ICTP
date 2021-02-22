using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Lab1ICTP
{
    public partial class Career
    {
        public int CareerId { get; set; }
        [Display(Name = "Команда")]
        public int TeamId { get; set; }
        [Display(Name = "Гравець")]
        public int PlayerId { get; set; }
        [Display(Name = "Початок кар'єри")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Кінець кар'єри")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Позиція")]
        public int PositionId { get; set; }

        [Display(Name = "Гравець")]
        public virtual Player Player { get; set; }
        [Display(Name = "Позиція")]
        public virtual Position Position { get; set; }
        [Display(Name = "Команда")]
        public virtual Team Team { get; set; }
    }
}
