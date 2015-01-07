using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcAutomation.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name="Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name="Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name="Повторите новый пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage="Пароли не совпали")]
        public string RepeatPassword { get; set; }
    }
}