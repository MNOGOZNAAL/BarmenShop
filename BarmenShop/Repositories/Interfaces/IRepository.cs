namespace BarmenShop.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task CreateAsync(T item);
        void Update(T item);
        void Remove(T item);
        Task SaveAsync();
    }
}
