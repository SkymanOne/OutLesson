using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OutLesson.DataLayer.ObjectModels
{
	public class ApplicationUser : IdentityUser
	{
		public int Year { get; set; }
		public string FullName { get; set; }
		public virtual List<Post> Posts { get; set; }
	}
}