using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Lab1ICTP.ViewModels
{

    public class ForgotPasswordViewModel
        {
        [Required(ErrorMessage = "Порожнє поле")]
        [EmailAddress]
        public string Email { get; set; }
        }
}
