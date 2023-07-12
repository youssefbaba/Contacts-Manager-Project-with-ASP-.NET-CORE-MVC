using DataLayer.Models;

namespace ServiceContracts
{
    public interface IWeatherService
    {
        // Methods
        List<CityWeather> GetWeatherDetails();

        CityWeather? GetWeatherByCityCode(string cityCode);
    }
}