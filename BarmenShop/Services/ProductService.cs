using BarmenShop.Entites;
using BarmenShop.Models.ViewModels;
using BarmenShop.Models.ViewModels.Product;
using BarmenShop.Repositories.Interfaces;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFilterService<Product> _filterService;
        public ProductService(IProductRepository productRepository, IImageService imageService,
                UserManager<User> userManager, IHttpContextAccessor httpContextAccessor,
                IFilterService<Product> filterService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _filterService = filterService;
        }
        public async Task Create(ProductCreateViewModel model)
        {
            User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (user == null)
            {
                return;
            };

            string path = await _imageService.SaveImage(model.ImageFile, user.UserName);

            Product category = new Product()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                ImageLink = path,
                UserCreatorId = user.Id,
                CreationDate = DateTime.Now,
            };
            await _productRepository.CreateAsync(category);
            await _productRepository.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Remove(product);
                await _productRepository.SaveAsync();
            }
        }

        public async Task Update(ProductUpdateViewModel model)
        {
            Product product = _productRepository.GetById(model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;
                product.Price = model.Price;
                if (model.ImageFile != null)
                {
                    string path = await _imageService.SaveImage(model.ImageFile, "updated");
                    product.ImageLink = path;
                }
                _productRepository.Update(product);
                await _productRepository.SaveAsync();
            }
        }

        public async Task<ProductIndexViewModel> GetAllAsync(int page, int? categoryId, int pageSize = 12)
        {
            if (page < 1) page = 1;
            IQueryable<Product> source;
            source = _productRepository.GetAll().OrderBy(x => x.CreationDate);
            if (categoryId != null)
            {
                source = _filterService.FilterCategory(source, (int)categoryId);
            }
            var count = await source.CountAsync();
            IQueryable<Product> items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            ProductIndexViewModel model = new ProductIndexViewModel
            {
                Products = await items.ToListAsync(),
                PageViewModel = pageViewModel,
            };
            return model;
        }

        public ProductDetailsViewModel GetDetailsModel(int id)
        {
            Product product = _productRepository.GetById(id);
            return new ProductDetailsViewModel() { Product = product };
        }

        public ProductUpdateViewModel GetUpdateModel(int id)
        {
            Product product = _productRepository.GetById(id);
            return new ProductUpdateViewModel()
            {   
                CategoryId = product.CategoryId,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<ProductIndexViewModel> FindBy(string text)
        {
            List<Product> products = await _productRepository.GetAll().Where(x =>
            x.Name.ToLower().Contains(text.ToLower()) || 
            x.Category.Name.ToLower().Contains(text.ToLower()) || 
            x.Description.ToLower().Contains(text.ToLower())).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(products.Count, 1, 12);

            ProductIndexViewModel model = new ProductIndexViewModel
            {
                Products = products,
                PageViewModel = pageViewModel,
            };
            return model;
        }
    }
}
