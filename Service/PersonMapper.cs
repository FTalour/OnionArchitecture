using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class PersonMapper
    {

        public static Person MapToDomain(PersonData personData)
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

        public static Person MapToDomain(PersonCsv personData)
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

        public static PersonCsv MapToCsv(Person person)
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
    }
}
