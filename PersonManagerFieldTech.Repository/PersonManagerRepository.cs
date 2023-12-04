using PersonManagerFieldTech.Data.Model;
using PersonManagerFieldTech.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonManagerFieldTech.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _persons;

        public PersonRepository()
        {
            _persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
                new Person { Id = 2, FirstName = "Jane", LastName = "Doe", Age = 25 },
            };
        }

        public IQueryable<Person> GetPersons()
        {
            return _persons.AsQueryable();
        }

        public Person? GetPersonById(int id)
        {
            return _persons.FirstOrDefault(p => p.Id == id);
        }

        public int CreatePerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            person.Id = GenerateUniqueId();
            _persons.Add(person);
            return person.Id;
        }

        public bool UpdatePerson(int id, Person updatedPerson)
        {
            if (updatedPerson == null)
                throw new ArgumentNullException(nameof(updatedPerson));

            var existingPerson = _persons.FirstOrDefault(p => p.Id == id);

            if (existingPerson == null)
                return false;

            // Update properties
            existingPerson.FirstName = updatedPerson.FirstName;
            existingPerson.LastName = updatedPerson.LastName;
            existingPerson.Age = updatedPerson.Age;

            return true;
        }

        public bool DeletePerson(int id)
        {
            var personToRemove = _persons.FirstOrDefault(p => p.Id == id);

            if (personToRemove == null)
                return false;

            _persons.Remove(personToRemove);
            return true;
        }

        private int GenerateUniqueId()
        {
            // Simulate generating a unique identifier
            return _persons.Count + 1;
        }
    }

}