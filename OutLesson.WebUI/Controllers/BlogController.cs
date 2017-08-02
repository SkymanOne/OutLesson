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

namespace OutLesson.WebUI.Controllers
{
    public class BlogController : Controller
	{ 

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }


		[HttpGet]
		[Authorize(Roles = "user")]
		[ValidateAntiForgeryToken]
	    public ActionResult OfferPost()
	    {

		    return View();
	    }

    }
}