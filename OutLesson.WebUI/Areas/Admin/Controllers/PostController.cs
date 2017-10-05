using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Areas.Admin.Models;

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
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var post = _unitOfWork.Posts.Get(id.Value);
                Mapper.Initialize(cfg => cfg.CreateMap<Post, PostModel>());
                return View(Mapper.Map<Post, PostModel>(post));
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(PostModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<PostModel, Post>());
                var post = Mapper.Map<PostModel, Post>(model);
                _unitOfWork.Posts.Update(post);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(model);
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