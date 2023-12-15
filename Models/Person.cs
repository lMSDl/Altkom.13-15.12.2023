namespace Models
{
    public class Person
    {
        public Person() { }
        public int Id { get; set; }

        public bool IsDeleted { get; set; }


        /*private string _firstName = string.Empty;

public string GetFirstName()
{
return _firstName;
}
public void SetFirstName(string firstName)
{
_firstName = firstName;
}*/

        //auto-property
        //możemy obniżać poziom dostępności dla gettera lub settera
        //możemy nadawać wartość początkową po znaku =
        public string FirstName { get; /*private*/ set; } = string.Empty;

        //backfield dla full-property
        //przyjmuje wartość początkową (zamiast property)
        private string lastName = string.Empty;
        //full-property
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                //value = nazwa parametry settera
                lastName = value.ToUpper();
                Console.WriteLine("Zmieniono nazwisko");
            }
        }


        public int Age 
        { 
            get
            {
                return DateTime.Now.Year - BirthDate.Year;
            }
        }

        //Read-only property - bez settera. Może zwracać jakieś zagregowane dane lub być ustawiony tylko w konstruktorze (final after constructor)
        public DateTime BirthDate { get; set; }

        public Person(string firstName, string lastName, DateTime birthDate) : this(birthDate)
        {
            FirstName= firstName;
            LastName = lastName;
        }
        public Person(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        public string FullName => $"{FirstName} {LastName}"; // FistName + " " + LastName

        public void DoSth()
        {
            //BirthDate = new DateTime();
            //Age = 1;
        }

    }
}