using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationContext() : base("OutLesson")
		{
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<OfferPost> OfferPosts { get; set; }

		public static ApplicationContext Create()
		{
			return new ApplicationContext();
		}


	}
}