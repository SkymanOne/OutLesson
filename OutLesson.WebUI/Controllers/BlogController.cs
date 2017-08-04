using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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



		//TODO: Определиться с правами доступа на запрос!
		[HttpGet]
		[Authorize(Roles = "user, writer, moder, admin")]
	    public ActionResult OfferPost()
	    {
		    return View();
	    }


		[HttpPost]
		[Authorize(Roles = "user, writer, moder, admin")]
		public ActionResult OfferResult(OfferPostModel model)
		{
			if (ModelState.IsValid)
			{
				var offerPost = Mapper.Map<OfferPostModel, OfferPost>(model);
			}


			return View(model);
		}

    }
}