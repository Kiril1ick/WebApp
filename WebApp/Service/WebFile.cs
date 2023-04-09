using System.Security.Cryptography;
using System.Text;
using System.IO;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace WebApp.Service
{
    public class WebFile
    {
        public WebFile() 
        {

        }

        public string GetWebFileName(string fileName)
        {
            string dir = GetWebFileFolder(fileName);
            CreateFolder(dir);
            return dir + "/" + Path.GetFileNameWithoutExtension(fileName)+".jpeg";
        }
        public string GetWebFileFolder(string fileName)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputByte = Encoding.ASCII.GetBytes(fileName);
            byte[] hashByte = md5hash.ComputeHash(inputByte);

            string hash = Convert.ToHexString(hashByte);

            return "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }
        public async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
        {
            using(Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectHeight = (int)(image.Width / (image.Height / (float)newHeight));
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));
                await image.SaveAsJpegAsync(fileName, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}
