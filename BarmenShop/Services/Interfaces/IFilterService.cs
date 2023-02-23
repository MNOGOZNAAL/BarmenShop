namespace BarmenShop.Services.Interfaces
{
    public interface IFilterService<T>
    {
        IQueryable<T> FilterCategory(IQueryable<T> list, int categoryId);
    }
}
