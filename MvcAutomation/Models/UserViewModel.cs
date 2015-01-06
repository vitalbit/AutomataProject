using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAutomation.Models
{
    public class UserViewModel
    {
        [Required]
        [Remote("CheckNickName", "Registration")]
        [Display(Name = "Никнейм")]
        public string Nickname { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Почтовый адрес")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Введите каптчу")]
        public string Captcha { get; set; }
    }
}