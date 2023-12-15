using Models;

namespace Services.Interfaces
{
    public interface IGenericService<T>
    {
        int Create(T entity);
        T? Read(int id);
        IEnumerable<T> Read();
        void Update(int id, T entity);
        bool Delete(int id);
    }
}