using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;

namespace ContactsManager.Core.ServiceContracts
{
    public interface IPersonsSorterService
    {
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
    }
}
