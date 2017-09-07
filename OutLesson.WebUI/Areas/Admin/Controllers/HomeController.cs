using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Areas.Admin.Models;
using OutLesson.WebUI.Models;
using OutLesson.WebUI.Utilities;

namespace OutLesson.WebUI.Areas.Admin.Controllers
{

	[Authorize(Roles = "admin, moder, writer")]
    public class HomeController : Controller
    {
	    private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

	    private readonly UnitOfWork _unitOfWork = new UnitOfWork();

	    readonly CreateShortUrl createShortUrl = new CreateShortUrl();

		// GET: Admin/Home
		[HttpGet]
        public ActionResult Index()
		{
			OfferPostRepository offerPosts = _unitOfWork.OfferPosts;
			//TODO:

			ViewBag.Text = createShortUrl.ReplaceString("Какой-то текст!");

			return View(offerPosts);
        }


	    [HttpGet]
		[ChildActionOnly]
	    public ActionResult GetPreviewPost(OfferPost offerPost)
	    {
		    ViewBag.Text = "какой-то текст";

		    return PartialView("GetPreviewPost", offerPost);
	    }

		[HttpGet]
	    public ActionResult CreatePost()
	    {
		    return View();
	    }

		[HttpPost]
		public ActionResult CreatePost(CreatePostModel model)
		{
			if (ModelState.IsValid)
			{
				Mapper.Initialize(c => c.CreateMap<CreatePostModel, Post>());
				var currentUser = _unitOfWork.DataContext.Users.Find(User.Identity.GetUserId());

				model.Autor = currentUser;
				model.ShortUrl = createShortUrl.ReplaceString(model.Title); 

				var post = Mapper.Map<CreatePostModel, Post>(model);

				_unitOfWork.Posts.Create(post);
				_unitOfWork.Save();

				return View("SuccessCreatePost", currentUser);
			}
			return View(model);
		}
		
    }
}