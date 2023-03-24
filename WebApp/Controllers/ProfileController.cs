using Microsoft.AspNetCore.Mvc;
using WebApp.Middleware;
using WebApp.ViewModel;
using WebApp.Service;

namespace WebApp.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
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
