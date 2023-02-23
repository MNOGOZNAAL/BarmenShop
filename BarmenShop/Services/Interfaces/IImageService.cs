namespace BarmenShop.Services.Interfaces
{
    public interface IImageService
    {
        public Task<string> SaveImage(IFormFile file, string name);
    }
}
