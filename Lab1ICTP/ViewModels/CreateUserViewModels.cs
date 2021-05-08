using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Lab1ICTP.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage ="Поле не повинно бути порожнім")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} повинно мати мінімум {2} і максимум {1} символів.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }
    }
}
