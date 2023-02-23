using BarmenShop.Entites;

namespace BarmenShop.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAll();
    }
}
