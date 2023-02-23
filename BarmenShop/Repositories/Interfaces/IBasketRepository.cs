using BarmenShop.Entites;

namespace BarmenShop.Repositories.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Basket GetBasketById(int id);
        Basket GetBasketByUserId(string userId);
        BasketItem GetBacketItemByBasketAndProductId(int basketId, int productId);
        Task CreateItem(BasketItem basketItem);
        void UpdateItem(BasketItem basketItem);
        void RemoveItem(BasketItem basketItem);
    }
}
