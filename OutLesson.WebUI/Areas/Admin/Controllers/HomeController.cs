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


		// GET: Admin/Home
		[HttpGet]
        public ActionResult Index()
		{
			OfferPostRepository offerPosts = _unitOfWork.OfferPosts;
			//TODO: реализовать вывод постов на модерацию

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
        public ActionResult EditAboutUs()
        {
            var aboutUs = _unitOfWork.DataContext.AboutUs.Find(1);
            if (aboutUs == null)
            {
                aboutUs = new AboutUs()
                {
                    Description = "<p>Edit this text pls!</p>",
                    TimeChange = DateTime.Today
                };

                _unitOfWork.DataContext.AboutUs.Add(aboutUs);
                _unitOfWork.Save();
            }
            return View(aboutUs);
        }

        [HttpPost]
        public ActionResult EditAboutUs(AboutUs model)
        {
            if (ModelState.IsValid)
            {
                model.TimeChange = DateTime.Today;
                _unitOfWork.DataContext.Entry(model).State = EntityState.Modified;
                _unitOfWork.Save();
                return RedirectToAction("About", "Home", new {area = ""});
            }
            return View(model);
        }
		
    }
}