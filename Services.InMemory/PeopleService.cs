using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    //: - implementacja interfejsu
    public class PeopleService : IPeopleService
    {
        private readonly ICollection<Person> _people;

        public PeopleService()
        {
            _people = new List<Person>();
        }

        public int Create(Person entity)
        {
            /*int maxId = 0;
            foreach (Person person in _people)
            {
                if(person.Id > maxId)
                    maxId = person.Id;
            }

            entity.Id = maxId + 1;*/

            //_people.Select(x => x.Id).Max();
            entity.Id = _people.Max(x => x.Id) + 1;

            _people.Add(entity);

            return entity.Id;
        }

        public bool Delete(int id)
        {
            Person? person = Read(id);
            if(person == null)
                return false;

            _people.Remove(person);
            return true;
        }

        public Person? Read(int id)
        {
            /*foreach(Person person in _people)
            {
                if(id == person.Id)
                {
                    return person;
                }
            }

            return null;*/
            //return _people.FirstOrDefault(x => x.Id == id);
            return _people.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> Read()
        {
            //return new List<Person>(_people);
            return _people.ToList();
        }

        public void Update(int id, Person entity)
        {
            if(Delete(id))
            {
                entity.Id = id;
                _people.Add(entity);
            }

            //Person person = Read(id);
            //person.FirstName = entity.FirstName;
            //...
            //person.BirthDate = entity.BirthDate;

        }
    }
}