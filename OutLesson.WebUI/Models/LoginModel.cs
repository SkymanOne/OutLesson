using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OutLesson.WebUI.Models
{
	public class LoginModel
	{
		[Required]
		public string Email { get; set; }
		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}