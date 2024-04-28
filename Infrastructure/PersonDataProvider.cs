using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class PersonDataProvider
    {
        private readonly string connectionString;

        public PersonDataProvider(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<PersonData> GetPersons()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var cmdText = @"
                            SELECT [BusinessEntityID] Id
                                ,[Title] Title
                                ,[FirstName] FirstName
                                ,[LastName] LastName
                                ,[ModifiedDate] ModifiedDate
                            FROM [Person].[Person]";

                return conn.Query<PersonData>(cmdText).ToList();
            }
        }
    }
}
