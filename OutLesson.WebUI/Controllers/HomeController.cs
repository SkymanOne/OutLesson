﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OutLesson.DataLayer;
using OutLesson.WebUI.Models;
using OutLesson.DataLayer.ObjectModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer.Repositories;

namespace OutLesson.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly UnitOfWork _unitOfWork = new UnitOfWork();

		[HttpGet]
		public ActionResult Index()
		{
			var list = _unitOfWork.Posts.GetAll();
			return View(list);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}


		[Authorize(Roles = "admin, moder")]
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}