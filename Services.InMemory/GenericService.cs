using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    //: - implementacja interfejsu
    public class GenericService<T> : IGenericService<T> where T : Entity
    {
        protected ICollection<T> _entities;

        public GenericService()
        {
            _entities = new List<T>();
        }

        public virtual int Create(T entity)
        {
            /*int maxId = 0;
            foreach (T e in _entities)
            {
                if(e.Id > maxId)
                    maxId = e.Id;
            }

            entity.Id = maxId + 1;*/

            //entity.Id = (_entities.Any() ? _entities.Max(x => x.Id) : 0) + 1;
            entity.Id = _entities.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            _entities.Add(entity);

            return entity.Id;
        }

        public virtual bool Delete(int id)
        {
            T? entity = Read(id);
            if(entity == null)
                return false;

            //_entities.Remove(entity);
            entity.IsDeleted = true;
            return true;
        }

        public T? Read(int id)
        {
            /*foreach(Person entity in _entities)
            {
                if(id == entity.Id)
                {
                    return entity;
                }
            }

            return null;*/
            //return _entities.FirstOrDefault(x => x.Id == id);
            return _entities.Where(x => !x.IsDeleted).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> Read()
        {
            //return new List<T>(_entities);
            return _entities.Where(x => !x.IsDeleted).ToList();
        }

        public virtual void Update(int id, T entity)
        {
            if(Delete(id))
            {
                entity.Id = id;
                _entities.Add(entity);
            }
        }
    }
}