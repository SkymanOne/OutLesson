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
			//TODO: реализовать вывод постов на модерацию

			ViewBag.Text = createShortUrl.ReplaceString("Какой-то текст!");

			return RedirectToAction("Index", "Post");
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
		public ActionResult CreatePost(PostModel model)
		{
			if (ModelState.IsValid)
			{
				Mapper.Initialize(c => c.CreateMap<PostModel, Post>());
				var currentUser = _unitOfWork.DataContext.Users.Find(User.Identity.GetUserId());

				model.Autor = currentUser;
                string shortUrl = createShortUrl.ReplaceString(model.Title);

			    Post checkShortUrl = null;
                try
			    {
			        checkShortUrl = _unitOfWork.Posts.GetAll().Single(s => s.ShortUrl == shortUrl);
			    }

			    catch (InvalidOperationException e)
			    {
			        checkShortUrl = null;
			    }

			    if (checkShortUrl != null)
			    {
			        ModelState.AddModelError("", "Заголовки не должны совпадать");
			    }

			    model.ShortUrl = shortUrl;

                

				var post = Mapper.Map<PostModel, Post>(model);

				_unitOfWork.Posts.Create(post);
				_unitOfWork.Save();

				return View("SuccessCreatePost", currentUser);
			}
			return View(model);
		}
		
    }
}