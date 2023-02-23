using BarmenShop.Entites;
using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Order;
using BarmenShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarmenShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Create(OrderCreateViewModel model)
        {
            Answer asnwer = await _orderService.Create(model);
            return new JsonResult(Ok(asnwer));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _orderService.GetUserOrders();
            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllOrders()
        {
            List<Order> orders = await _orderService.GetAllOrders();
            return View("Index",orders);
        }
    }
}
