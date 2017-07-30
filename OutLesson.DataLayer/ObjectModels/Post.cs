using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OutLesson.DataLayer.ObjectModels
{
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		[AllowHtml]
		public string Content { get; set; }
		public DateTime Time { get; set; }
		public virtual ApplicationUser Autor { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
	}
}
