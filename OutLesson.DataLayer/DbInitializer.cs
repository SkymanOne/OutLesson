using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer
{
	public class DbInitializer : DropCreateDatabaseAlways<ApplicationContext>
	{
		protected override void Seed(ApplicationContext context)
		{
			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			//роль админа
			var adminRole = new ApplicationRole
			{
				Name = "admin",
				Description = "Роль адмиинистратора для управлением веб-приложением"
			};

			//роль модеротра
			var moderRole = new ApplicationRole
			{
				Name = "moder",
				Description = "Роль модератора для управлением обыкновенных пользователей и проверки, написанного контента"
			};

			//роль редактора
			var writerRole = new ApplicationRole
			{
				Name = "writer",
				Description = "Роль редактора (писателя), имевшая возможность писать статьи без модерации"
			};

			//роль обычного пользователя
			var userRole = new ApplicationRole {Name = "user", Description = "Роль обыкновенного пользователя"};

			//регистрируем роли
			roleManager.Create(adminRole);
			roleManager.Create(moderRole);
			roleManager.Create(writerRole);
			roleManager.Create(userRole);

			//регистриуем админа
			var admin = new ApplicationUser
			{
				Email = "german.nikolishin@gmail.com",
				UserName = "german.nikolishin@gmail.com",
				FullName = "SkymanOne"
			};
			var adminPass = "gn_vneuroka";
			var adminResult = userManager.Create(admin, adminPass);

			var moder = new ApplicationUser
			{
				Email = "nikgolubtsov@gmail.com",
				UserName = "nikgolubtsov@gmail.com",
				FullName = "nikgolubtsov"
			};
			var moderPass = "ng_vneuroka";
			var moderResult = userManager.Create(moder, moderPass);

			if (adminResult.Succeeded & moderResult.Succeeded)
			{
				userManager.AddToRoles(admin.Id, adminRole.Name);

				userManager.AddToRoles(moder.Id, moderRole.Name);
			}

			base.Seed(context);
		}
	}
}