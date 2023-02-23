using BarmenShop.Entites;
using BarmenShop.Models.Responces;
using BarmenShop.Repositories.Interfaces;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarmenShop.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        public BasketService(IBasketRepository basketRepository, UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }
        public async Task<Answer> AddProduct(int productId)
        {
            Product product = _productRepository.GetById(productId);
            User user = null;
            if (_httpContextAccessor.HttpContext?.User?.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            if (user == null)
            {
                return new Answer() { Success = false, Message = $"Необходима регистрация" };
            }
            Basket basket = await GetMyBasket();
            if (basket == null)
            {
                basket = new Basket()
                {
                    UserId = user.Id,
                };
                await _basketRepository.CreateAsync(basket);
                await _basketRepository.SaveAsync();
            }

            BasketItem basketItem = _basketRepository.GetBacketItemByBasketAndProductId(basket.Id, product.Id);
            if (basketItem == null)
            {
                BasketItem newBasketItem = new()
                {
                    ProductId = product.Id,
                    BacketId = basket.Id,
                    Amount = 1
                };
                await _basketRepository.CreateItem(newBasketItem);
                await _basketRepository.SaveAsync();
            }
            else
            {
                basketItem.Amount += 1;
                _basketRepository.UpdateItem(basketItem);
                await _basketRepository.SaveAsync();
            }
            return new Answer() { Success = true, Message = $"Успешно добавлено в корзину" };
        }

        public async Task<Basket> GetMyBasket()
        {
            User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            return _basketRepository.GetBasketByUserId(user.Id);
        }

        public async Task<Answer> RemoveProduct(int productId)
        {
            Product product = _productRepository.GetById(productId);
            User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (user == null)
            {
                return new Answer() { Success = false, Message = $"Необходима регистрация" };
            }
            Basket basket = await GetMyBasket();
            BasketItem basketItem = _basketRepository.GetBacketItemByBasketAndProductId(basket.Id, product.Id);
            if (basketItem == null)
            {
                return new Answer() { Success = false, Message = $"Этот товар уже удален" };
            }
            else
            {
                _basketRepository.RemoveItem(basketItem);
                await _basketRepository.SaveAsync();
            }

            if (basket.BacketItems.Count <= 0)
            {
                _basketRepository.Remove(basket);
                await _basketRepository.SaveAsync();
            }

            return new Answer() { Success = true, Message = $"Товар успешн удален" };
        }
    }
}
