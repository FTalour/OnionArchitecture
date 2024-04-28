using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
namespace Infrastructure
{
    public class PersonCsv
    {
        [Name("Id")]
        public int Id { get; set; }
        [Name("GivenName", "FirstName")]
        public string? FirstName { get; set; }
        [Name("LastName")]
        public string? LastName { get; set; }
        [Name("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }
        [Name("Age")]
        public int Age { get; set; }
    }
}
