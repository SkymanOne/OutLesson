using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.WebUI.Models
{
	public class UserModel
	{

        [HiddenInput]
        public string Id { get; set; }


		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Ник маслёнка")]
		public string FullName  { get; set; }

		[Required]
		public string Email { get; set; }

		[Display(Name = "Возраст маслёнка")]
		public int Year { get; set; }

		[Display(Name = "Телефон")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Введи пароль этого счастливчика")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "АХАХА, пароли то не совпали")]
		[Display(Name = "Подтверди свой мега сложный пароль, я оценю это")]
		public string ConfirmPassword { get; set; }
	}

    public class EditUserModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Ник")]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Возраст")]
        public int Year { get; set; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтверди свой мега сложный пароль, я оценю это")]
        public string ConfirmNewPassword { get; set; }
    }
}