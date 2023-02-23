using BarmenShop.Database;
using BarmenShop.Entites;
using BarmenShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly BarmenAppDbContext _context;

        public UserRepository(UserManager<User> userManager, BarmenAppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _userManager.CreateAsync(user);
        } 

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public User GetById(string userId)
        {
            return _context.Users.Include(x => x.Orders).ThenInclude(x => x.Basket).ThenInclude(x => x.BacketItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == userId);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.Include(x => x.Orders).ThenInclude(x => x.Basket);
        }
        public User GetByLogin(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }
    }
}
