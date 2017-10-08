using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.WebUI.Models
{
	public class OfferPostModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Заголовок")]
		public string Title { get; set; }
		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Содержание")]
		[AllowHtml]
		public string Content { get; set; }
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Дата публикации")]
		public DateTime Time { get; set; }

		[HiddenInput]
		public ApplicationUser Autor { get; set; }

		//TODO: релизовать добавление тэгов
	}
}