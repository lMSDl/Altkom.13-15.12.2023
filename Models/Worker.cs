using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Worker : Person
    {
        public Worker()
        {
        }

        public Worker(DateTime birthDate) : base(birthDate)
        {
        }

        public Worker(string firstName, string lastName, DateTime birthDate) : base(firstName, lastName, birthDate)
        {
        }

        public string Specialisation { get; set; }
    }
}
