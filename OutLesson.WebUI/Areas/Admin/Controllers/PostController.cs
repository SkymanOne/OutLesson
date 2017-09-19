using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;

namespace OutLesson.WebUI.Areas.Admin.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для взаимодействия со списком постов
    /// </summary>
    [Authorize(Roles = "admin, moder, writer")]
    public class PostController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Admin/Post

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Post> postList = _unitOfWork.Posts.GetAll();

            if (postList != null)
                return View(postList);
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Delete(int? id, string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                if (id != null)
                {
                    var post = _unitOfWork.Posts.Get(id.Value);
                    return View(post);
                }
                return HttpNotFound();
            }
            else
            {
                var post = _unitOfWork.Posts.GetAll().Single(u => u.ShortUrl.Equals(url));
                return View(post);
            }
        }


        [HttpPost]
        public ActionResult Delete(string url)
        {
            var post = _unitOfWork.Posts.GetAll().Single(u => u.ShortUrl.Equals(url));
            _unitOfWork.Posts.Delete(post.Id);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Post");
        }

    }
}