using BarmenShop.Entites;
using BarmenShop.Models.Responces;

namespace BarmenShop.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetMyBasket();
        Task<Answer> AddProduct(int productId);
        Task<Answer> RemoveProduct(int productId);
    }
}
