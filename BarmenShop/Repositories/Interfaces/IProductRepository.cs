using BarmenShop.Entites;

namespace BarmenShop.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetById(int id);
        IQueryable<Product> GetAll();
    }
}
