using BarmenShop.Entites;
using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Basket;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarmenShop.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<IActionResult> MyBasket()
        {
            Basket basket = await _basketService.GetMyBasket();
            if (basket == null)
            {
                return PartialView("PartialViews/Basket", null);
            }
            decimal total = 0;
            foreach (var item in basket.BacketItems)
            {
                total += item.Product.Price * item.Amount;
            }
            BasketViewModel model = new BasketViewModel()
            {
                Basket = basket,
                TotalSum = total
            };
            return PartialView("PartialViews/Basket", model);
        }

        [HttpPost]
        public async Task<JsonResult> AddProduct(int productId)
        {
            Answer answer = await _basketService.AddProduct(productId);
            return new JsonResult(Ok(answer));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> RemoveProduct(int productId)
        {
            Answer answer = await _basketService.RemoveProduct(productId);
            return new JsonResult(Ok(answer));
        }
    }
}
