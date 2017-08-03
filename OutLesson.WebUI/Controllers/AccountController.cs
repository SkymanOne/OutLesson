using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OutLesson.DataLayer;
using OutLesson.WebUI.Models;
using OutLesson.DataLayer.ObjectModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutLesson.WebUI.Controllers
{
    public class AccountController : Controller
    {
		private ApplicationUserManager ApplicationUserManager
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		} 

		private IAuthenticationManager AuthenticationManager
		{
			get
			{   
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> Index()
		{
			ApplicationUser user = null;
			user = await ApplicationUserManager.FindByEmailAsync(User.Identity.Name);

			return View(user);
		}

		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}


		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = null;

				user = await ApplicationUserManager.FindAsync(model.Email, model.Password);
				if (user != null)
				{
					ClaimsIdentity claim = await ApplicationUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
					AuthenticationManager.SignOut();
					AuthenticationManager.SignIn(new AuthenticationProperties
					{
						IsPersistent = true
					}, claim);
					if (String.IsNullOrEmpty(returnUrl))
						return RedirectToAction("Index", "Home");
					return Redirect(returnUrl);
				}
				else
				{
					ModelState.AddModelError("", "Неверный логин или пароль");
				}
			}
			ViewBag.ReturnUrl = returnUrl;

			return View();
		}

		public ActionResult LogOut()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Login");
		}

    }
}