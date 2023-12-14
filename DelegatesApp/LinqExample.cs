using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesApp
{
    internal class LinqExample
    {
        int[] numbers = new[] { 1, 2, 3, 4, 8, 3, 9, 4, 10 };
        IEnumerable<string> strings = "ala ma kota i dwa psy".Split(' ').ToList();
        IEnumerable<Person> people = new List<Person>
        {
            new Person("Adam", "Adamski", DateTime.Now.AddYears(-35)),
            new Person("Ewa", "Adamska", DateTime.Now.AddYears(-23)),
            new Person("Ewa", "Ewowska", DateTime.Now.AddYears(-43)),
            new Person("Adam", "Ewowski", DateTime.Now.AddYears(-12)),
            new Person("Marcin", "Adamski", DateTime.Now.AddYears(-56)),
            new Person("Marcin", "Marcinkowski", DateTime.Now.AddYears(-15)),
            new Person("Marcin", "Ewowski", DateTime.Now.AddYears(-90)),
        };


        public void Test()
        {
            List<int> values = new List<int>();
            foreach (var number in numbers)
            {
                if(number > 4)
                    values.Add(number);
            }

            
            //method syntax
            IEnumerable<int> result = numbers.Where(number => number > 4).ToList();
            result = numbers.Where(number => number > 4).OrderByDescending(x => x).ToList();

            //query syntax
            result = from number in numbers where number > 4 select number;
            result = from number in numbers where number > 4 orderby number descending select number;

            IEnumerable<string> stringResults = strings.Where(x => x.Length > 3).Where(x => x.Contains("a")).ToList();

            IEnumerable<Person> peopleResult = people.Where(x => x.Age > 35).ToList();


            stringResults = people.Where(x => x.LastName == "EWOWSKI").Select(x => $"{x.FirstName} {x.LastName}").ToList();
            foreach (var item in stringResults)
            {
                Console.WriteLine(item);
            }
            stringResults.ToList().ForEach(x => Console.WriteLine(x));


            //single używamy gdy mamy pewność, że na liście jest tylko jeden obiekt spełniający warunek
            //Person person = people.Single(x => x.FirstName == "Ewa");

            //first używamy na liście może być więcej niż jeden obiekt spełniający warunek
            //rezultatem jest piewszy znaleziony
            Person person = people.First(x => x.FirstName == "Ewa");

            //SingleOrDefault lub FirstOrDefault - zwracają null jeśli nie znaleziono obiektu
            person = people.FirstOrDefault(x => x.FirstName == "Edward");

            double averageAge = people.Select(x => x.Age).Average();

            averageAge = people.Skip(2).Take(3).Select(x => x.Age).Max();


        }


    }
}
