using ServiceContracts;

namespace ServiceLayer
{
    public class CitiesService : ICitiesService, IDisposable
    {
        // Some business logic here ( business calculations + business validations )

        // Fields
        private List<string> _cities;

        private Guid _serviceInstanceId;

        // Properties
        public Guid ServiceInstanceId
        {
            get
            {
                return _serviceInstanceId;
            }
        }

        // Constructors
        public CitiesService()
        {
            _serviceInstanceId = Guid.NewGuid();

            _cities = new List<string>()
            {
                "London",
                "New York",
                "Tokyo",
                "Madrid",
                "Paris"
            };

            // TO DO: Add logic to open db connection

        }


        // Methods
        public List<string> GetCities()
        {
            return _cities;
        }

        public void Dispose()
        {
            // TO DO: Add logic to close db connection
        }
    }
}