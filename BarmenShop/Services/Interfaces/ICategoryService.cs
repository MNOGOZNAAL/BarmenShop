using BarmenShop.Entites;
using BarmenShop.Models.ViewModels.Category;

namespace BarmenShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task Create(CategoryCreateViewModel model);
        Task Remove(int id);
        Task Update(CategoryUpdateViewModel model);
        Task<CategoryIndexViewModel> GetAllAsync(int page, int pageSize = 12);
        CategoryDetailsViewModel GetDetailsModel(int id);
        Task<List<Category>> GetAll();
    }
}
