using Models;
using Services.InMemory;
using Services.Interfaces;

IPeopleService peopleService = new PeopleService();

Person p = new Person("Ewa", "Ewowska", new DateTime(1990, 1, 1));
peopleService.Create(p);

p = new Person("Adam", "Adamski", new DateTime(1980, 12, 24));
peopleService.Create(p);


IEnumerable<Person> people = peopleService.Read();

foreach(Person person in people)
{
    Console.WriteLine($"{person.Id}\t{person.FirstName}\t{person.LastName}\t{person.Age}");
}
