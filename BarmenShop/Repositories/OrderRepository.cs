using BarmenShop.Database;
using BarmenShop.Entites;
using BarmenShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BarmenAppDbContext _context;
        public OrderRepository(BarmenAppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders.Include(x => x.Basket).ThenInclude(x => x.BacketItems).ThenInclude(x => x.Product).Include(x => x.User);
        }

        public void Remove(Order item)
        {
            _context.Orders.Remove(item);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Order item)
        {
            _context.Orders.Update(item);
        }
    }
}
