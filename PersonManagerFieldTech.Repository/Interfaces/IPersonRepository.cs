using PersonManagerFieldTech.Data.Model;

namespace PersonManagerFieldTech.Repository.Interfaces
{
    public interface IPersonRepository
    {
        int CreatePerson(Person person);
        bool DeletePerson(int id);
        Person? GetPersonById(int id);
        IQueryable<Person> GetPersons();
        bool UpdatePerson(int id, Person updatedPerson);
    }
}