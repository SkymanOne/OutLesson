using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OutLesson.DataLayer.ObjectModels
{
	class ApplicationUser : IdentityUser
	{
		public int Year { get; set; }
		public string FullName { get; set; }
		public virtual List<Post> Posts { get; set; }
	}
}
