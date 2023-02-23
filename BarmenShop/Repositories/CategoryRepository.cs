using BarmenShop.Database;
using BarmenShop.Entites;
using BarmenShop.Repositories.Interfaces;

namespace BarmenShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BarmenAppDbContext _context;
        public CategoryRepository(BarmenAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(Category item)
        {
            _context.Categories.Remove(item);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Category item)
        {
            _context.Categories.Update(item);
        }
    }
}
