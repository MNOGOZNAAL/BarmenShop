using BarmenShop.Database;
using BarmenShop.Entites;
using BarmenShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BarmenAppDbContext _context;
        public BasketRepository(BarmenAppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Basket item)
        {
            await _context.Baskets.AddAsync(item);
        }

        public async Task CreateItem(BasketItem basketItem)
        {
            await _context.BasketItems.AddAsync(basketItem);
        }

        public BasketItem GetBacketItemByBasketAndProductId(int basketId, int productId)
        {
            return _context.BasketItems.Include(x => x.Product).Include(x => x.Backet).FirstOrDefault(x => x.BacketId == basketId && x.ProductId == productId);
        }

        public Basket GetBasketById(int id)
        {
            return _context.Baskets.Include(x => x.User).Include(x => x.BacketItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
        }

        public Basket GetBasketByUserId(string userId)
        {
            return _context.Baskets.Include(x => x.User).Include(x => x.BacketItems).ThenInclude(x => x.Product).Where(x => x.IsOrdered == false).FirstOrDefault(x => x.UserId == userId);
        }

        public void Remove(Basket item)
        {
            _context.Baskets.Remove(item);
        }

        public void RemoveItem(BasketItem basketItem)
        {
            _context.BasketItems.Remove(basketItem);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Basket item)
        {
            _context.Baskets.Update(item);
        }

        public void UpdateItem(BasketItem basketItem)
        {
            _context.BasketItems.Update(basketItem);
        }
    }
}
