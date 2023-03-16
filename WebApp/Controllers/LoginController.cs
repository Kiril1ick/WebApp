using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.BL.Auth;
using WebApp.Middleware;
using WebApp.ViewMapper;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuthBL authBL;
        public LoginController(IAuthBL authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            try
            {
                await authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch (BL.Exceptions.AuthorizationException)
            {
                ModelState.AddModelError("Email", "Имя или Email не найден");
            }

            return View("Index", model);
        }
    }
}
