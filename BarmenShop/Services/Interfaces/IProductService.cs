using BarmenShop.Models.ViewModels.Product;

namespace BarmenShop.Services.Interfaces
{
    public interface IProductService
    {
        Task Create(ProductCreateViewModel model);
        Task Remove(int id);
        Task Update(ProductUpdateViewModel model);
        Task<ProductIndexViewModel> GetAllAsync(int page, int? categoryId, int pageSize = 12);
        ProductDetailsViewModel GetDetailsModel(int id);
        ProductUpdateViewModel GetUpdateModel(int id);
    }
}
