using Microsoft.Data.SqlClient;
using Dapper;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace simpleProjet
{
    public class Person
    {
        public int Id { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public string? Title { get; set; }
        
        [CsvHelper.Configuration.Attributes.Name("GivenName", "FirstName")]
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

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var cmdText = @"
                            SELECT [BusinessEntityID] Id
                                ,[Title] Title
                                ,[FirstName] FirstName
                                ,[LastName] LastName
                                ,[ModifiedDate] ModifiedDate
                            FROM [Person].[Person]
                            where Title is not null";

                return conn.Query<Person>(cmdText).ToList();
            }
        }

        public void ExportPersons(IEnumerable<Person> people, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(people);
                }
            }
        }

        public IEnumerable<Person> ImportPersons(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Person>().ToList();
                }
            }
        }
    }
}