using PersonManagerFieldTech.Services.ViewModels;

namespace PersonManagerFieldTech.Services.Services.Interfaces
{
    public interface IPersonService
    {
        Task<int> CreatePerson(PersonViewModel newPerson);
        Task<bool> DeletePerson(int id);
        Task<PersonViewModel?> GetPersonById(int id);
        Task<IEnumerable<PersonViewModel>> GetPersons();
        Task<bool> UpdatePerson(int id, PersonViewModel updatedPerson);
    }
}