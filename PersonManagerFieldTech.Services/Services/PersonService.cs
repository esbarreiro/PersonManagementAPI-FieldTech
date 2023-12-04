using PersonManagerFieldTech.Data.Model;
using PersonManagerFieldTech.Repository.Interfaces;
using PersonManagerFieldTech.Services.Services.Interfaces;
using PersonManagerFieldTech.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagerFieldTech.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonViewModel>> GetPersons()
        {
            return _personRepository.GetPersons().Select(p => new PersonViewModel()
            {
                Age = p.Age,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Id = p.Id,
            }).ToList();
        }

        public async Task<PersonViewModel?> GetPersonById(int id)
        {
            var person = _personRepository.GetPersonById(id);

            if (person == null)
                return null;

            return new PersonViewModel()
            {
                Age = person.Age,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Id = person.Id,
            };
        }

        public async Task<int> CreatePerson(PersonViewModel newPerson)
        {
            if (newPerson == null)
                throw new ArgumentNullException(nameof(newPerson));

            Person person = new Person()
            {
                Age = newPerson.Age,
                FirstName = newPerson.FirstName,
                LastName = newPerson.LastName,
                Id = newPerson.Id,
            };

            var personId = _personRepository.CreatePerson(person);
            return personId;
        }

        public async Task<bool> UpdatePerson(int id, PersonViewModel updatedPerson)
        {
            if (updatedPerson == null)
                throw new ArgumentNullException(nameof(updatedPerson));

            Person person = new Person()
            {
                Age = updatedPerson.Age,
                FirstName = updatedPerson.FirstName,
                LastName = updatedPerson.LastName
            };

            return _personRepository.UpdatePerson(id, person);
        }

        public async Task<bool> DeletePerson(int id)
        {
            var personToRemove = _personRepository.GetPersonById(id);

            if (personToRemove == null)
                return false;

            return _personRepository.DeletePerson(id);
        }

    }
}
