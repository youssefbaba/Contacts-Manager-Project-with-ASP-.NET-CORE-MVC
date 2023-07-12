using DataLayer.Models;
using ServiceContracts;

namespace ServiceLayer
{
    public class WeatherService : IWeatherService
    {

        // Methods
        public List<CityWeather> GetWeatherDetails()
        {
            return GetData().ToList();
        }

        public CityWeather? GetWeatherByCityCode(string? cityCode)
        {
            CityWeather? cityWeather = GetData().FirstOrDefault(temp => string.Equals(temp.CityUniqueCode, cityCode, StringComparison.OrdinalIgnoreCase));
            return cityWeather;
        }

        private static IEnumerable<CityWeather> GetData()
        {
            return new List<CityWeather>()
            {
                new CityWeather()
                {
                    CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33
                },
                new CityWeather()
                {
                    CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60
                },
                new CityWeather()
                {
                    CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82
                }
            };
        }
    }
}