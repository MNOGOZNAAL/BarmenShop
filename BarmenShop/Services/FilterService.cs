using BarmenShop.Entites;
using BarmenShop.Services.Interfaces;

namespace BarmenShop.Services
{
    public class FilterService : IFilterService<Product>
    {
        public IQueryable<Product> FilterCategory(IQueryable<Product> list, int categoryId)
        {
            return list.Where(x => x.CategoryId == categoryId);
        }
    }
}
