using BarmenShop.Entites;
using BarmenShop.Models.Responces;
using BarmenShop.Models.ViewModels.Order;

namespace BarmenShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Answer> Create(OrderCreateViewModel model);
        Task<List<Order>> GetUserOrders();
        Task<List<Order>> GetAllOrders();
    }
}
