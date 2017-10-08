using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Areas.Admin.Models;
using OutLesson.WebUI.Models;

namespace OutLesson.WebUI.Areas.Admin.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для взаимодествия с пользователями
    /// </summary>
	[Authorize(Roles = "admin, moder, writer")]
	public class UserController : Controller
    {
	    private ApplicationUserManager UserManager => 
			HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

	    private ApplicationRoleManager RoleManager =>
		    HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();

	    private readonly UnitOfWork _unitOfWork = new UnitOfWork();


		[HttpGet]
		public ActionResult GetUsers()
		{
			var users = UserManager.Users;
			return View(users);
		}

		[HttpGet]
		public ActionResult RegisterUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RegisterUser(UserModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Year == 0)
				{
					ModelState.AddModelError("", "Вводи возраст числом!");
				}

				ApplicationUser checkUser = await UserManager.FindByEmailAsync(model.Email);

				if (checkUser == null)
				{
					try
					{
						checkUser = _unitOfWork.DataContext.Users.Single(s => s.FullName == model.FullName);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						checkUser = null;
					}
					if (checkUser == null)
					{

						ApplicationUser user = new ApplicationUser
						{
							UserName = model.Email,
							Email = model.Email,
							FullName = model.FullName,
							Year = model.Year,
							PhoneNumber = model.PhoneNumber
						};
						var result = await UserManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							await UserManager.AddToRolesAsync(user.Id, "writer");
							return View("SuccessRegisterUser", user);
						}
						else
						{
							ModelState.AddModelError("", "Ошибка регистрации пользователя");
							ModelState.AddModelError("", result.ToString());
						}
					}
					else
					{
						ModelState.AddModelError("", "Пользователь с таким ником существует");
					}
				}
				else
				{
					ModelState.AddModelError("", "Пользователь с таким Email существует");
				}

			}
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> DeleteUser(string id)
		{
			if (UserManager.IsInRole(id, "superadmin"))
			{
				return new HttpStatusCodeResult(423);
			}
			var user = await UserManager.FindByIdAsync(id);
			return View(user);
		}

		[HttpPost]
		public async Task<ActionResult> DeleteUser(string id, string returnUrl)
		{
			var user = await UserManager.FindByIdAsync(id);

			var result = await UserManager.DeleteAsync(user);

			if (result.Succeeded)
				return RedirectToAction("GetUsers");

			return new HttpStatusCodeResult(432);
		}

		[HttpGet]
		public async Task<ActionResult> DetailsUser(string id)
		{
			var user = await UserManager.FindByIdAsync(id);
			return View(user);
		}


		[HttpGet]
		public async Task<ActionResult> EditUser(string id)
		{
			Mapper.Initialize(c => c.CreateMap<ApplicationUser, UpdateUserModel>());

		    if (UserManager.IsInRole(id, "superadmin"))
		        return new HttpStatusCodeResult(432);

		    var user = _unitOfWork.DataContext.Users.Find(id);

		    if (user == null)
		        return HttpNotFound();

			var userModel = Mapper.Map<ApplicationUser, UpdateUserModel>(user);


			return View(userModel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<UpdateUserModel, ApplicationUser>());
                var passwordHash = new PasswordHasher();
                var user = Mapper.Map<UpdateUserModel, ApplicationUser>(model);
                user.UserName = user.Email;
                if (!String.IsNullOrWhiteSpace(model.NewPasssword))
                {
                    user.PasswordHash = passwordHash.HashPassword(model.NewPasssword);
                }

                _unitOfWork.DataContext.Entry(user).State = EntityState.Modified;
                _unitOfWork.Save();

                return RedirectToAction("DetailsUser", "User", new {id = user.Id});
            }
            return View(model);
        }

	}
}