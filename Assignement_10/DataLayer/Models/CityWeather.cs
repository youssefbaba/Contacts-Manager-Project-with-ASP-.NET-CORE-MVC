using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class CityWeather
    {
        public string? CityUniqueCode { get; set; }

        public string? CityName { get; set; }

        public DateTime DateAndTime { get; set; }

        public int TemperatureFahrenheit { get; set; }
    }
}
