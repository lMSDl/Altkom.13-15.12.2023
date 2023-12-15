using Models;

namespace Services.Interfaces
{
    public interface IGenericAsyncService<T>
    {
        Task<int> CreateAsync(T entity);
        Task<T?> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}