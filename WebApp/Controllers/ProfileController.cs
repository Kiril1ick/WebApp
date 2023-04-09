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
            var profiles = await currentUser.GetProfiles();

            ProfileModel? profileModel = profiles.FirstOrDefault();

            return View(profileModel != null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async  Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userId = await currentUser.GetCurrentUserId();
            if (userId == null) throw new Exception("Пользователь не найден");
            var profiles = await profileBL.Get((int)userId);
            if (model.ProfileId!=null && !profiles.Any(m => m.ProfileId == model.ProfileId))
                throw new Exception("Error");


            if (ModelState.IsValid)
            {
                ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                profileModel.UserId = (int)userId;
                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    WebFile webFile = new WebFile();
                    string fileName = webFile.GetWebFileName(Request.Form.Files[0].FileName);
                    await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), fileName, 800, 600);
                    profileModel.ProfileImage = fileName;
                }
                await profileBL.AddOrUpdate(profileModel);
                return Redirect("/");
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
