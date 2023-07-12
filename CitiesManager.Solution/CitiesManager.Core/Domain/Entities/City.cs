using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CitiesManager.Core.Domain.Entities
{
    public class City
    {
        [XmlElement("cityID")]
        public Guid CityID { get; set; }

        [XmlElement("cityName")]
        [Required(ErrorMessage = "City Name can't be blank.")]
        public string? CityName { get; set; }
    }
}
