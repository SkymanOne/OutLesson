using System;
using System.Collections.Generic;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	internal class UserRepository : IRepostory<ApplicationUser>
	{
		private ApplicationUserManager userManager;

		public UserRepository(ApplicationUserManager context)
		{
			userManager = context;
		}

		//TODO: realize interfase methods

		public void Create(ApplicationUser item)
		{
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public ApplicationUser Get(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ApplicationUser> GetAll()
		{
			throw new NotImplementedException();
		}

		public void Update(ApplicationUser item)
		{
			throw new NotImplementedException();
		}
	}
}