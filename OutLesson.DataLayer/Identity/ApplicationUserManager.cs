﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
			: base(store)
		{
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
			IOwinContext context)
		{
			var db = context.Get<ApplicationContext>();
			var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));

			manager.PasswordValidator = new PasswordValidator()
			{
				RequireLowercase = true,
				RequiredLength = 6,
			};

			return manager;
		}
	}
}