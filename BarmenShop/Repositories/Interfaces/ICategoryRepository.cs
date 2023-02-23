using BarmenShop.Entites;

namespace BarmenShop.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetById(int id);
        IQueryable<Category> GetAll();
    }
}
