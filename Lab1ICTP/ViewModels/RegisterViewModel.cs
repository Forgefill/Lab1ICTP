﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Lab1ICTP.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Порожнє поле")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Порожнє поле")]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Порожнє поле")]
        [Display(Name ="Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} повинно мати мінімум {2} і максимум {1} символів.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Порожнє поле")]
        [Compare("Password", ErrorMessage ="Паролі не співпадають")]
        [Display(Name ="Підтвердження паролю")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
