using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OutLesson.DataLayer.ObjectModels;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OutLesson.WebUI.Areas.Admin.Models
{
	public class CreatePostModel
	{
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Заголовок")]
		public string Title { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Краткое описание")]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Содержимое")]
		public string Content { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Display(Name = "Дата публикации")]
		public DateTime Time { get; set; }

		[HiddenInput]
		public string ShortUrl { get; set; }

		[HiddenInput]
		public ApplicationUser Autor { get; set; }
	}
}