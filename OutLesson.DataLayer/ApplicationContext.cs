using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext() : base("DefaultConnection") { }

		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }

		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}
	}
}
