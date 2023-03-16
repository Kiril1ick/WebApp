using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WebApp.Middleware;
using WebApp.ViewModel;

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
            string fileName = "";
            var imgData = Request.Form.Files[0];

            if (imgData != null)
            {
                MD5 md5hash = MD5.Create();
                byte[] inputByte = System.Text.Encoding.ASCII.GetBytes(imgData.FileName);
                byte[] hashByte = md5hash.ComputeHash(inputByte);

                string hash = Convert.ToHexString(hashByte);

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                fileName = dir + "/" + imgData.FileName;
                using (var stream = System.IO.File.Create(fileName))
                    await imgData.CopyToAsync(stream);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
