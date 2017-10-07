using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OutLesson.DataLayer;
using OutLesson.DataLayer.ObjectModels;
using OutLesson.DataLayer.Repositories;
using OutLesson.WebUI.Models;

namespace OutLesson.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        private ApplicationUserManager ApplicationUserManager =>
            HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var model = new UserInfoModel();


            var user = await ApplicationUserManager.FindByEmailAsync(User.Identity.Name);
            model.User = user;
            var posts = _unitOfWork.Posts.GetAll().Where(s => s.Autor.FullName == user.FullName);

            model.UserPosts = posts;


            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;

                user = await ApplicationUserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    var claim =
                        await ApplicationUserManager.CreateIdentityAsync(user,
                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}