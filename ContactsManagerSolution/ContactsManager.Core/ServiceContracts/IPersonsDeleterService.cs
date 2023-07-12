
namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsDeleterService
    {
        Task<bool> DeletePerson(Guid? personID);
    }
}
