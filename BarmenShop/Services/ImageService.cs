using BarmenShop.Services.Interfaces;

namespace BarmenShop.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImage(IFormFile file, string name)
        {
            DirectoryInfo dirInfo = new DirectoryInfo("wwwroot/Files");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string extention = Path.GetExtension(file.FileName);
            string path = $"/Files/{name}-{Guid.NewGuid()}{extention}";

            using (var fileStream = new FileStream("wwwroot" + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}
