using Models;
using Services.InFile.Encryption;
using Services.Interfaces;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services.InFile
{
    public class PeopleService : Services.InMemory.PeopleService
    {
        private readonly string _path;
        private AsymmetricEncryption? _encryption;
        private readonly string _certName;

        public PeopleService(string path, string? certName)
        {
            _path = path;
            _encryption = new AsymmetricEncryption();
            _certName = certName;
            _entities = LoadData();
        }
        public PeopleService(string path) : this(path, null)
        {

        }

        private ICollection<Person> LoadData()
        {
            if(File.Exists(_path))
            {
               string json;
                if (_certName == null)
                    json = File.ReadAllText(_path);
                else
                    json = _encryption.DecryptToString(File.ReadAllBytes(_path), _certName);
                
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
            string json = JsonSerializer.Serialize(_entities);
            if (_certName == null)
                File.WriteAllText(_path, json);
            else
                File.WriteAllBytes(_path, _encryption.Encrypt(json, _certName));
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

        public override int Create(Person entity)
        {
            //base - odwołanie się do implementacji bazowej
            int id = base.Create(entity);
            SaveData();
            return id;
        }

        public override bool Delete(int id)
        {
            bool isDeleted = base.Delete(id);
            if (isDeleted)
                SaveData();
            return isDeleted;
        }

        public override void Update(int id, Person entity)
        {
            base.Update(id, entity);
            SaveData();
        }

    }
}