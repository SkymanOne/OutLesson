using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer;

namespace OutLesson.DataLayer
{
	class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
	{
		protected override void Seed(ApplicationContext context)
		{

			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			var adminRole = new ApplicationRole { Name = "admin", Description = "Роль адмиинистратора для управлением веб-приложением" };
			var moderRole = new ApplicationRole { Name = "moder", Description = "Роль модератора для управлением обыкновенных пользователей и проверки, написанного контента" };
			var writerRole = new ApplicationRole { Name = "writer", Description = "Роль редактора (писателя), имевшая возможность писать статьи без модерации" };
			var userRole = new ApplicationRole { Name = "user", Description = "Роль обыкновенного пользователя" };

			roleManager.Create(adminRole);
			roleManager.Create(moderRole);
			roleManager.Create(writerRole);
			roleManager.Create(userRole);

			var admin = new ApplicationUser { Email = "german.nikolishin@gmail.com", UserName = "german.nikolishin@gmail.com", FullName = "SkymanOne" };
			string pass = "gn_vneuroka";
			var result = userManager.Create(admin, pass);

			base.Seed(context);
		}
	}
}
