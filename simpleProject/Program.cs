using System.Globalization;
using Domain;
using Service;

namespace simpleProjet
{
    

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
        private PersonService personService;
        public static void Main(params string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            string connectionString = "Server=localhost;Database=AdventureWorks2022;Integrated Security = true;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            personService = new PersonService(connectionString);

            var people = GetPersons();

            ShowPersons(people);

            UpdatePeopleAge(people);

            string peopleFilePath = "people.csv";
            ExportPersons(people, peopleFilePath);
            var importedPersons = ImportPersons(peopleFilePath);

            ShowPersons(importedPersons);
        }

        private IEnumerable<Person> GetPersons()
        {
            return personService.GetPersons();
        }

        private IEnumerable<Person> ImportPersons(string peopleFilePath)
        {
            return personService.ImportPersons(peopleFilePath);
        }

        private void ExportPersons(IEnumerable<Person> people, string peopleFilePath)
        {
            personService.ExportPersons(people, peopleFilePath);
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
    }
}