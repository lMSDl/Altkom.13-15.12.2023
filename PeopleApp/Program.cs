using Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

CultureInfo a = System.Globalization.CultureInfo.CurrentUICulture;

Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de");

string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
path = Path.Combine(path, "people.json");

IPeopleService peopleService = new Services.InFile.PeopleService(path, "CN=localhost");

Initialize();

bool exit = false;

while (!exit)
{
    Console.Clear();

    ShowPeople();
    ShowMenu();

    char input = Console.ReadKey().KeyChar;
    switch (input)
    {
        case '1':
            Add();
            break;

        case '2':
            Delete();
            break;

        case '3':
            Edit();
            break;

        case '4':
            exit = true;
            break;

        case '5':
            ToJson();
            break;

        case '6':
            ToXML();
            break;

        case '7':
            ToNewtonSoftJson();
            break;

        case '8':
            ToNewtonSoftXML();
            break;

        default:
            Console.WriteLine();
            Console.WriteLine("Błędna komenda");
            Console.ReadLine();
            break;
    }

}

void ToNewtonSoftXML()
{
    int id = RequestForId();
    Person person = peopleService.Read(id);

    string json = JsonConvert.SerializeObject(person);
    XDocument xDocument =  JsonConvert.DeserializeXNode(json, nameof(Person));
    string xml = xDocument.ToString();
    Console.WriteLine(xml);
    Console.ReadLine();
}

void ToNewtonSoftJson()
{
    int id = RequestForId();
    Person person = peopleService.Read(id);

    JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
    jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    //jsonSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
    jsonSettings.DateFormatString = "MM yy ddd";
    jsonSettings.TypeNameHandling = TypeNameHandling.All;
    
    string json = JsonConvert.SerializeObject(person, jsonSettings);
    Console.WriteLine(json);
    Console.ReadLine();
}

void ToXML()
{
    int id = RequestForId();
    Person person = peopleService.Read(id);

    XmlSerializer xmlSerializer = new XmlSerializer(person.GetType());

    using MemoryStream memoryStream = new MemoryStream();
    xmlSerializer.Serialize(memoryStream, person);

    string xml = Encoding.Default.GetString(memoryStream.ToArray());

    //memoryStream.Dispose();

    Console.WriteLine(xml);
    Console.ReadLine();


}

void ToJson()
{
    int id = RequestForId();
    Person person = peopleService.Read(id);

    JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
    jsonOptions.IgnoreReadOnlyProperties = true;
    jsonOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString;
#if DEBUG
    jsonOptions.WriteIndented = true;
#endif

    string json = System.Text.Json.JsonSerializer.Serialize(person, jsonOptions);

    Console.WriteLine(json);
    Console.ReadLine();
}

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("1. " + PeopleApp.Properties.Resources.Add);
    Console.WriteLine("2. " + PeopleApp.Properties.Resources.Remove);
    Console.WriteLine("3. " + PeopleApp.Properties.Resources.Edit);
    Console.WriteLine("4. " + PeopleApp.Properties.Resources.End);
    Console.WriteLine("5. JSON");
    Console.WriteLine("6. XML");
    Console.WriteLine("7. NewtonSoft JSON");
    Console.WriteLine("8. Newtonsoft XML");
}

void ShowPeople()
{
    IEnumerable<Person> people = peopleService.Read();
    people = people.OrderBy(x => x.Id).ToList();
    foreach (Person person in people)
    {
        Console.WriteLine($"{person.Id,-3}{person.FirstName,-15}{person.LastName,-25}{person.Age,-4}");
    }
}

void Initialize()
{
    if (peopleService.Read().Any())
        return;

    Person p = new Person("Ewa", "Ewowska", new DateTime(1990, 1, 1));
    peopleService.Create(p);

    p = new Person("Adam", "Adamski", new DateTime(1980, 12, 24));
    peopleService.Create(p);

    p = new Person("Wojeciech", "Wojeciechowski", new DateTime(2000, 1, 4));
    peopleService.Create(p);
}

void Delete()
{
    int id = RequestForId();
    _ = peopleService.Delete(id);
}

void Add()
{
    Console.WriteLine();

    string firstName = RequestForData(PeopleApp.Properties.Resources.PutFirstName);
    string lastName = RequestForData(PeopleApp.Properties.Resources.PutLastName);

    string birthDate;
    do
    {
        birthDate = RequestForData("Podaj datę urodzenia:");
    } while (!DateTime.TryParse(birthDate, out _)); // _ - discard, czyli ignorujemy / nie insteresuje nas ten parametr

    Person newPerson = CreatePerson(firstName, lastName, birthDate);
    _ = peopleService.Create(newPerson);
}

Person CreatePerson(string firstName, string lastName, string birthDate)
{
    Person? newPerson;
    do
    {
        Console.WriteLine("1. Osoba; 2. Student; 3. Pracownik");
        char input = Console.ReadKey().KeyChar;
        switch (input)
        {
            case '1':
                newPerson = new Person(firstName,
                                              lastName,
                                              DateTime.Parse(birthDate));
                break;
            case '2':
                newPerson = new Student(firstName, lastName, DateTime.Parse(birthDate)) { IndexNumber = RequestForData("Numer indeksu:") };
                break;

            case '3':
                newPerson = new Worker(firstName, lastName, DateTime.Parse(birthDate)) { Specialisation = RequestForData("Specjalizacja:") };
                break;
            default:
                newPerson = null;
                break;
        }
    } while (newPerson == null);
    return newPerson;
}



void Edit()
{
    Person person;
    do
    {
        int id = RequestForId();
        person = peopleService.Read(id);
    } while (person == null);

    Console.WriteLine();
    string firstName = RequestForData(PeopleApp.Properties.Resources.PutFirstName, true);
    string lastName = RequestForData(PeopleApp.Properties.Resources.PutLastName, true);
    string birthDate = RequestForData("Podaj datę urodzenia:", true);

    Person newPerson = new Person(string.IsNullOrWhiteSpace(firstName) ? person.FirstName : firstName,
                                  string.IsNullOrWhiteSpace(lastName) ? person.LastName : lastName,
                                  DateTime.TryParse(birthDate, out DateTime db) ? db : person.BirthDate);

    peopleService.Update(person.Id, newPerson);
}

int RequestForId()
{
    Console.WriteLine();
    Console.Write("Podaj identyfikator: ");
    string value = Console.ReadLine();
    int id = int.Parse(value);
    return id;
}

//parametr opcjonale - parametr, który posiada przypisaną domyślną wartość i nie wymaga podawania w trakcie wywoływania funkcji
//parametry opcjonale muszą być na końcu listy parametrów
static string RequestForData(string label, bool allowEmpty = false)
{
    string data;
    do
    {
        Console.WriteLine(label);
        data = Console.ReadLine();
    } while (!allowEmpty && string.IsNullOrWhiteSpace(data));
    return data;
}