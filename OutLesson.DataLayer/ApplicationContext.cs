using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OutLesson.DataLayer
{
	class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext() : base("DefaultConnection") { }

		public DbSet<Post> Posts { get; set; }

		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}
	}
}
