using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab1ICTP.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Порожнє поле")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Порожнє поле")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} повинно мати мінімум {2} і максимум {1} символів.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
