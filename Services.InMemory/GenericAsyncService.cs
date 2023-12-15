using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    public class GenericAsyncService<T> : GenericService<T>, IGenericAsyncService<T> where T : Entity
    {
        public Task<int> CreateAsync(T entity)
        {
            return Task.FromResult(base.Create(entity));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.Run<bool>(() =>
            {
                return base.Delete(id);
            });
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.Run<T?>(() =>
            {
                return base.Read(id);
            });
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
           await Task.Yield();
           return base.Read();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await Task.Delay(1000);
            UpdateAsync(id, entity);
        }
    }
}