using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OutLesson.DataLayer;
using OutLesson.WebUI.Models;
using OutLesson.DataLayer.ObjectModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OutLesson.DataLayer.Repositories;

namespace OutLesson.WebUI.Controllers
{
	[Authorize]
	public class BlogController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

		public BlogController()
		{
			_unitOfWork = new UnitOfWork();
		}

		[HttpGet]
		public ActionResult OfferPost()
		{
			return View();
		}

		[HttpPost]
		public ActionResult OfferPost(OfferPostModel model)
		{
			if (ModelState.IsValid)
			{
				Mapper.Initialize(config => config.CreateMap<OfferPostModel, OfferPost>());
				var currentUser = _unitOfWork.DataContext.Users.Find(User.Identity.GetUserId());
				model.Autor = currentUser;
				var offerPost = Mapper.Map<OfferPostModel, OfferPost>(model);


				_unitOfWork.OfferPosts.Create(offerPost);
				_unitOfWork.Save();

				return View("Success", currentUser);
			}

			return View(model);
		}
	}
}