using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Areas.Admin.Models;
using OutLesson.WebUI.Utilities;

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
        readonly CreateShortUrl createShortUrl = new CreateShortUrl();

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Post> postList = _unitOfWork.Posts.GetAll().OrderByDescending(i => i.Time);

            if (postList != null)
                return View(postList);
            return HttpNotFound();
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

                //проверка наличия поста с таким же url
                var checkShortUrl = _unitOfWork.Posts.GetByUrl(shortUrl);
                if (checkShortUrl != null)
                {
                    ModelState.AddModelError("", "Заголовки не должны совпадать");
                    return View(model);
                }

                //проверка строки на null и пробелы
                if (String.IsNullOrWhiteSpace(shortUrl))
                {
                    ModelState.AddModelError("", "Некорректный заголовок");
                    return View(model);
                }

                model.ShortUrl = shortUrl;



                var post = Mapper.Map<PostModel, Post>(model);

                _unitOfWork.Posts.Create(post);
                _unitOfWork.Save();

                return View("SuccessCreatePost", currentUser);
            }
            return View(model);
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