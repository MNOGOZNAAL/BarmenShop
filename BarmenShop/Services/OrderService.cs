using BarmenShop.Entites;
using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Order;
using BarmenShop.Repositories.Interfaces;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IBasketRepository basketRepository,
            UserManager<User> userManager, IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Answer> Create(OrderCreateViewModel model)
        {
            User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (user == null)
            {
                return new Answer() { Success = false, Message = $"Необходима регистрация" };
            };

            Basket basket = _basketRepository.GetBasketById(model.BasketId);
            if (basket == null)
            {
                return new Answer() { Success = false, Message = $"Заказа для размещения не найдено" };
            }

            if (basket.UserId != user.Id)
            {
                return new Answer() { Success = false, Message = $"Это не ваш заказ" };
            }

            if (string.IsNullOrWhiteSpace(model.Contacts) || string.IsNullOrWhiteSpace(model.Address))
            {
                return new Answer() { Success = false, Message = $"Заполните адрес и контакты!" };
            }

            basket.IsOrdered = true;
            _basketRepository.Update(basket);
            await _basketRepository.SaveAsync();

            decimal totalSum = 0;

            foreach (var item in basket.BacketItems)
            {
                totalSum += item.Product.Price * item.Amount;
            }

            Order order = new()
            {
                BasketId = basket.Id,
                UserId = user.Id,
                TotalSum = totalSum,
                Address = model.Address,
                Contasts = model.Contacts,
                CreateDate = DateTime.Now,
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveAsync();
            return new Answer() { Success = true, Message = $"Заказ успешно размещен" };
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAll().OrderBy(x => x.CreateDate).ToListAsync();
        }

        public async Task<List<Order>> GetUserOrders()
        {
            User authorizedUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            User user = _userRepository.GetById(authorizedUser.Id);
            return user.Orders;
        }
    }
}
