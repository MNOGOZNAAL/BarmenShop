using BarmenShop.Database;
using BarmenShop.Entites;
using BarmenShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarmenShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BarmenAppDbContext _context;
        public ProductRepository(BarmenAppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.Include(x => x.Category);
        }

        public Product GetById(int id)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Product item)
        {
            _context.Products.Remove(item);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Product item)
        {
            _context.Products.Update(item);
        }
    }
}
