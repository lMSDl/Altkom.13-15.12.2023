using Models;
using Services.Interfaces;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services.InFile
{
    public class PeopleService : IPeopleService
    {
        private readonly ICollection<Person> _people;
        private readonly string _path;

        public PeopleService(string path)
        {
            _path = path;
            _people = LoadData();
        }

        private ICollection<Person> LoadData()
        {
            if(File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                return JsonSerializer.Deserialize<ICollection<Person>>(json) ?? new List<Person>();
            }
            else
            {
                return new List<Person>();
            }
        }

        /*private ICollection<Person> LoadData()
        {
            //klasy strumieniowe - zarządzają danymi na postawie stumienia byte, a nie właściwości jak zwykłe klasy
            FileStream fileStream = new FileStream(_path, FileMode.OpenOrCreate);
            //StreamReader / StreamWriter - klasy pomocnicze, które pozwalają nam operaować na stringach zamiast byte 
            StreamReader streamReader = new StreamReader(fileStream);

            string json = streamReader.ReadToEnd();

            //dispose - zwalnia zasoby zaalokowane/zablokowane przez klasy
            streamReader.Dispose();
            fileStream.Dispose();

            try
            {
                ICollection<Person>? result = JsonSerializer.Deserialize<ICollection<Person>>(json);
                return result ?? new List<Person>();
            }
            catch
            {
                return new List<Person>();
            }
        }*/

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(_people);
            File.WriteAllText(_path, json);
        }

        /*private void SaveData()
        {
            string json = JsonSerializer.Serialize(_people);


            //wykorzustanie using spowoduje automatyczne wywołanie funkcji Dispose
            using FileStream fileStream = new FileStream(_path, FileMode.OpenOrCreate);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            //wyłączenie automatycznego opróżniania bufora
            streamWriter.AutoFlush = false;

            streamWriter.Write(json);
            //metoda flush wymusza wypchnięcie danych do strumienia
            streamWriter.Flush();

        }*/


        public int Create(Person entity)
        {
            entity.Id = _people.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;

            _people.Add(entity);

            SaveData();

            return entity.Id;
        }

        public bool Delete(int id)
        {
            Person? person = Read(id);
            if (person == null)
            {
                return false;
            }

            person.IsDeleted = true;


            SaveData();
            return true;
        }

        public Person? Read(int id)
        {
            return _people.Where(x => !x.IsDeleted).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> Read()
        {
            return _people.Where(x => !x.IsDeleted).ToList();
        }

        public void Update(int id, Person entity)
        {
            if (Delete(id))
            {
                entity.Id = id;
                _people.Add(entity);

                SaveData();
            }

        }
    }
}