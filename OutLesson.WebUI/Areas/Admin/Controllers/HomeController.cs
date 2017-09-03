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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Models;

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
			//TODO: 

            return View(offerPosts);
        }


	    [HttpGet]
		[ChildActionOnly]
	    public ActionResult GetPreviewPost(OfferPost offerPost)
	    {
		    ViewBag.Text = "какой-то текст";

		    return PartialView("GetPreviewPost", offerPost);
	    }

    }
}