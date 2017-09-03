using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OutLesson.WebUI.Models
{
	public class UserModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Ник маслёнка")]
		public string FullName  { get; set; }

		[Required]
		public string Email { get; set; }

		
		[Display(Name = "Возраст маслёнка")]
		[DataType(DataType.PhoneNumber)]
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
		[Compare("Password", ErrorMessage = "АХАХА, пароли то не совпали")]
		[Display(Name = "Подтверди свой мега сложный пароль, я оценю это")]
		public string ConfirmPassword { get; set; }
	}
}