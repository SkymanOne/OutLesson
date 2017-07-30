using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutLesson.WebUI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "admin, moder")]
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