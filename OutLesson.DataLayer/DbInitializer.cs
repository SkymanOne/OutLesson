using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer
{
	public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
	{
		protected override void Seed(ApplicationContext context)
		{
			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			var superAdminRole = new ApplicationRole
			{
				Name = "superadmin",
				Description = "Роль супер-администратора"
			};

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
			roleManager.Create(superAdminRole);
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

			if (adminResult.Succeeded)
			{
				userManager.AddToRoles(admin.Id, adminRole.Name, superAdminRole.Name);
			}

			base.Seed(context);
		}
	}
}