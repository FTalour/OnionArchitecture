using System.Globalization;
using Infrastructure;

namespace simpleProjet
{
    public class Person
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int Age { get; set; }

        public void UpdateAge()
        {
            Age++;
        }

        public override string ToString()
        {
            string fullNameWithTitle;
            if (!string.IsNullOrWhiteSpace(Title))
            {
                fullNameWithTitle = $"{Title} {FirstName} {LastName}";
            }
            else
            {
                fullNameWithTitle = $"{FirstName} {LastName}";
            }
            return $"{Id} : {fullNameWithTitle} ({ModifiedDate})";
        }
    }

    /// <summary>
    /// Classe qui permet de 
    ///     Charger des personnes depuis la base de donnée
    ///     Exporter des personnes vers un format
    ///     Import des personnes depuis le même format
    ///     Modifie des personnes
    ///     Afficher des personnes
    /// </summary>
    public class Program
    {
        public static void Main(params string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            var people = GetPersons();

            ShowPersons(people);
            UpdatePeopleAge(people);

            string peopleFilePath = "people.csv";
            ExportPersons(people, peopleFilePath);
            var importedPersons = ImportPersons(peopleFilePath);

            ShowPersons(importedPersons);
        }



        public Person MapToDomain(PersonData personData)
        {
            return new Person()
            {
                Id = personData.Id,
                FirstName = personData.FirstName,
                LastName = personData.LastName,
                ModifiedDate = personData.ModifiedDate,
                Title = personData.Title,
            };
        }

        public Person MapToDomain(PersonCsv personData)
        {
            return new Person()
            {
                Id = personData.Id,
                FirstName = personData.FirstName,
                LastName = personData.LastName,
                ModifiedDate = personData.ModifiedDate,
                Age = personData.Age,
            };
        }

        public PersonCsv MapToCsv(Person person)
        {
            return new PersonCsv()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                ModifiedDate = person.ModifiedDate,
                Age = person.Age,
            };
        }

        public void ShowPersons(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
        }

        public void UpdatePeopleAge(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                person.UpdateAge();
            }
        }

        public IEnumerable<Person> GetPersons()
        {
            string connectionString = "Server=localhost;Database=AdventureWorks2022;Integrated Security = true;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            var personDataProvider = new PersonDataProvider(connectionString);
            return personDataProvider.GetPersons()
                .Select(MapToDomain);
        }

        public void ExportPersons(IEnumerable<Person> people, string filePath)
        {
            var peopleCsv = people.Select(MapToCsv).ToList();
            Infrastructure.CsvHelper.Write<PersonCsv>(peopleCsv, filePath);
        }

        public IEnumerable<Person> ImportPersons(string filePath)
        {
            return Infrastructure.CsvHelper.ReadCsv<PersonCsv>(filePath)
                .Select(MapToDomain);
        }
    }
}