using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsAdderService
    {
        Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);
    }
}
