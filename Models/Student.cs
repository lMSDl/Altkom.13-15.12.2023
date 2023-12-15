using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student : Person
    {
        public Student()
        {
        }

        public Student(DateTime birthDate) : base(birthDate)
        {
        }

        public Student(string firstName, string lastName, DateTime birthDate) : base(firstName, lastName, birthDate)
        {
        }

        public string IndexNumber { get; set; }

    }
}
