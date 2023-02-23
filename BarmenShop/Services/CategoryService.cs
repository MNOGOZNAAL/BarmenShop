using BarmenShop.Entites;
using BarmenShop.Models.ViewModels;
using BarmenShop.Models.ViewModels.Category;
using BarmenShop.Repositories.Interfaces;
using BarmenShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Create(CategoryCreateViewModel model)
        {
            Category category = new Category()
            {
                Name = model.Name,
                CreationDate = DateTime.Now,
            };
            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Category category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Remove(category);
                await _categoryRepository.SaveAsync();
            }
        }

        public async Task Update(CategoryUpdateViewModel model)
        {
            Category category = _categoryRepository.GetById(model.Id);
            if (category != null)
            {
                category.Name = model.Name;
                _categoryRepository.Update(category);
                await _categoryRepository.SaveAsync();
            }
        }
        public async Task<CategoryIndexViewModel> GetAllAsync(int page, int pageSize = 12)
        {
            if (page < 1) page = 1;
            IQueryable<Category> source;
            source = _categoryRepository.GetAll().OrderBy(x => x.CreationDate);

            var count = await source.CountAsync();
            IQueryable<Category> items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            CategoryIndexViewModel model = new CategoryIndexViewModel
            {
                Categories = await items.ToListAsync(),
                PageViewModel = pageViewModel,
            };
            return model;
        }

        public CategoryDetailsViewModel GetDetailsModel(int id)
        {
            Category category = _categoryRepository.GetById(id);
            return new CategoryDetailsViewModel() { Category = category };
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetAll().ToListAsync();
        }
    }
}
