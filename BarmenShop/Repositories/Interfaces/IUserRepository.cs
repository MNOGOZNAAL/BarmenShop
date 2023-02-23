using BarmenShop.Entites;

namespace BarmenShop.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(string userId);
        User GetByLogin(string username);
    }
}
