using BarmenShop.Models.ViewModels.Product;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarmenShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int page = 1)
        {
            ViewBag.SelectedCategory = categoryId;
            ProductIndexViewModel model = await _productService.GetAllAsync(page, categoryId);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _productService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Update(int id)
        {
            ProductUpdateViewModel model = _productService.GetUpdateModel(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(ProductUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _productService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            ProductDetailsViewModel model = _productService.GetDetailsModel(id);
            return View("Details", model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindBy(string text)
        {
            ProductIndexViewModel model = await _productService.FindBy(text);
            return View("Index",model);
        }
    }
}
