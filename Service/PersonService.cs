using Domain;
using Infrastructure;

namespace Service
{
    public class PersonService
    {
        private readonly string connectionString;

        public PersonService(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Person> GetPersons()
        {
            var personDataProvider = new PersonDataProvider(connectionString);
            return personDataProvider.GetPersons()
                .Select(PersonMapper.MapToDomain);
        }


        public void ExportPersons(IEnumerable<Person> people, string filePath)
        {
            var peopleCsv = people.Select(PersonMapper.MapToCsv).ToList();
            Infrastructure.CsvHelper.Write<PersonCsv>(peopleCsv, filePath);
        }

        public IEnumerable<Person> ImportPersons(string filePath)
        {
            return Infrastructure.CsvHelper.ReadCsv<PersonCsv>(filePath)
                .Select(PersonMapper.MapToDomain);
        }


    }
}
