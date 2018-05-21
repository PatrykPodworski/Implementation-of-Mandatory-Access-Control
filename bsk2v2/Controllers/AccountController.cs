using bsk2v2.Services;
using bsk2v2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bsk2v2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                var cleranceLevel = userService.GetCurrentUserCleranceLevel();

                var controlLevelService = new ControlLevelService(context);
                var controlLevels = controlLevelService.GetReadableFor(cleranceLevel);

                var model = new UserCreateViewModel(controlLevels);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                var result = await userService.Add(model);
                context.SaveChanges();

                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }

                AddErrors(result);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                var users = userService.GetAvailableUsers();

                var viewModel = new UserListViewModel(users);

                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                var user = userService.GetById(id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new UserDeleteViewModel(user);
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserDeleteViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                var userService = new UserService(context, HttpContext);
                userService.Delete(model);
                context.SaveChanges();

                return RedirectToAction("List");
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}