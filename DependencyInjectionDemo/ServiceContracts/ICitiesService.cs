namespace ServiceContracts
{
    public interface ICitiesService
    {
        // Properties
        Guid ServiceInstanceId { get; }

        // Methods
        List<string> GetCities();
    }
}