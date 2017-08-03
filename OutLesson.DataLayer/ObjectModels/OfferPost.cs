using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OutLesson.DataLayer.ObjectModels
{
	public class OfferPost
	{
		public int Id { get; set; }
		public string Title { get; set; }

		[AllowHtml]
		public string Content { get; set; }

		public DateTime Time { get; set; }

		public bool IsPublish { get; set; } = false;

		public virtual ApplicationUser WhoAcceptedPublish { get; set; }

		public virtual ApplicationUser Autor { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
	}
}
