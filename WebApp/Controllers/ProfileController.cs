using Microsoft.AspNetCore.Mvc;
using WebApp.Middleware;
using WebApp.ViewModel;
using WebApp.Service;
using WebApp.BL.Auth;
using WebApp.BL.Profile;
using WebApp.ViewMapper;
using WebApp.DAL.Models;

namespace WebApp.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        public readonly ICurrentUser currentUser;
        public readonly IProfileBL profileBL;
        public ProfileController(ICurrentUser currentUser, IProfileBL profileBL)
        {
            this.profileBL = profileBL;
            this.currentUser = currentUser;
        }
        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            int? userId = await currentUser.GetCurrentUser();
            var profiles = await profileBL.Get((int)userId);
            ProfileModel profileModel = profiles.FirstOrDefault();
            ProfileMapper.MapProfileModelToProfileViewModel(profileModel);
            return View();
        }

        [HttpPost]
        [Route("/profile")]
        public async  Task<IActionResult> IndexSave()
        {
            var imgData = Request.Form.Files[0];

            if (imgData != null)
            {
                WebFile webFile = new WebFile();
                string fileName = webFile.GetWebFileName(imgData.FileName);
                await webFile.UploadAndResizeImage(imgData.OpenReadStream(), fileName, 800, 600);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
