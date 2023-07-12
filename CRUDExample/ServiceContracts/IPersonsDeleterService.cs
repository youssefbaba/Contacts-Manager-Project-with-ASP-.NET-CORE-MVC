
namespace ServiceContracts
{
    public interface IPersonsDeleterService
    {
        Task<bool> DeletePerson(Guid? personID);
    }
}
