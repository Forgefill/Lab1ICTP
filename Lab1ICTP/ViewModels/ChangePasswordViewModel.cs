using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Lab1ICTP.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Порожнє поле")]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage ="Порожнє поле")]
        [StringLength(100, ErrorMessage = "Поле {0} повинно мати мінімум {2} і максимум {1} символів.", MinimumLength = 5)]
        public string NewPassword { get; set; }
    }
}
