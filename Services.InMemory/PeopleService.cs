using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    //: - implementacja interfejsu
    public class PeopleService : GenericService<Person>, IPeopleService
    {
        public IEnumerable<Person> ReadByLastName(string lastName)
        {
            return _entities.Where(x => x.LastName == lastName).ToList();
        }
    }
}