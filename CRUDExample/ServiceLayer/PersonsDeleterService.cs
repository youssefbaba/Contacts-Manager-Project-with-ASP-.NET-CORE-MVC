using Entities;
using RepositoryContracts;
using ServiceContracts;

namespace ServiceLayer
{
    public class PersonsDeleterService : IPersonsDeleterService
    {
        // Fields
        private readonly IPersonsRepository _personsRepository;

        // Constructors
        public PersonsDeleterService(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }

        // Methods
        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
            {
                throw new ArgumentNullException(nameof(personID));
            }

            // Regular Linq Query
            Person? matchingPerson = await _personsRepository.GetPersonByPersonID(personID.Value);

            if (matchingPerson == null)
            {
                return false;
            }

            // Regular Linq Query
            await _personsRepository.DeletePerson(personID.Value);

            /*
            // Stored Procedure
            _db.ProcedureDeletePerson((Guid)personID);
            */

            return true;
        }
    }
}



