using Models;

namespace Services.Interfaces
{
    public interface IPeopleService
    {
        int Create(Person entity);
        Person? Read(int id);
        IEnumerable<Person> Read();
        void Update(int id, Person entity);
        bool Delete(int id);
    }
}