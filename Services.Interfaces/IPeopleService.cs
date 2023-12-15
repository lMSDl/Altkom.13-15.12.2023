using Models;

namespace Services.Interfaces
{
    public interface IPeopleService : IGenericService<Person>
    {
        IEnumerable<Person> ReadByLastName(string lastName);
    }
}