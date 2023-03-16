using Microsoft.AspNetCore.Mvc;
using WebApp.BL.Auth;
using WebApp.BL.Exceptions;
using WebApp.Middleware;
using WebApp.ViewMapper;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController:Controller
    {
        private readonly IAuthBL authBL;
        public RegisterController(IAuthBL authBL) 
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index",new RegistrationViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Register(AuthMapper.MapRegistrationViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.TryAddModelError("Email","Email уже существует");
                }
            }
            return View("Index", model);
        }
    }
}
