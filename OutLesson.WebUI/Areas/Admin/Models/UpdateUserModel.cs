using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OutLesson.WebUI.Areas.Admin.Models
{
    public class UpdateUserModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Ник маслёнка")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Возраст маслёнка")]
        [DataType(DataType.PhoneNumber)]
        public int Year { get; set; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Введи пароль этого счастливчика")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "АХАХА, пароли то не совпали")]
        [Display(Name = "Подтверди свой мега сложный пароль, я оценю это")]
        public string ConfirmPassword { get; set; }
    }
}