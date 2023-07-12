using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    /// <summary>
    /// Person domain model class
    /// </summary>
    public class Person   // Child Model Class
    {
        // Properties

        [Key]  // PersonID is Primarykey
        public Guid PersonID { get; set; }

        [StringLength(40)]  // nvarchar(40)
        //[Required]        // PersonName should not be null or empty
        public string? PersonName { get; set; }

        [StringLength(40)]  // nvarchar(40)
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]  // nvarchar(10)
        public string? Gender { get; set; }

        // of type uniqueidentifier in SQL
        public Guid? CountryID { get; set; }  // ForeignKey

        [StringLength(200)]  // nvarchar(200)
        public string? Address { get; set; }

        // of type bit in SQL
        public bool ReceiveNewsLetters { get; set; }

        public string? TIN { get; set; }

        public string? SSN { get; set; }

        //public string? SSA { get; set; }

        public bool SSNIsChanged { get; set; }

        // Navigation Property
        [ForeignKey(nameof(CountryID))]  
        public virtual Country? Country { get; set; }

        // Methods
        public override string ToString()
        {
            return $"Person ID: {PersonID}, Person Name: {PersonName}, Email: {Email}, Date Of Birth: {DateOfBirth?.ToString("yyyy/MM/dd")}, Gender: {Gender}," +
                $" CountryID: {CountryID}, Country: {Country?.CountryName}, Address: {Address}, ReceiveNewsLetters: {ReceiveNewsLetters}";
        }
    }
}
